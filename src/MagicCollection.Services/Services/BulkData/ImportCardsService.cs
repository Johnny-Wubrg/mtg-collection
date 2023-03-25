using MagicCollection.Data;
using MagicCollection.Data.Entities;
using MagicCollection.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using ScryNet.Models;

namespace MagicCollection.Services.BulkData;

/// <inheritdoc />
public class ImportCardsService : IImportCardsService
{
  private readonly DbContextOptions<MagicCollectionContext> _contextOptions;
  private readonly ITaxonomyRepository<Treatment> _treatmentRepository;
  private readonly ITaxonomyRepository<Rarity> _rarityRepository;
  private readonly ITaxonomyRepository<Language> _languageRepository;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="contextOptions"></param>
  /// <param name="treatmentRepository"></param>
  /// <param name="rarityRepository"></param>
  /// <param name="languageRepository"></param>
  public ImportCardsService(
    DbContextOptions<MagicCollectionContext> contextOptions,
    ITaxonomyRepository<Treatment> treatmentRepository,
    ITaxonomyRepository<Rarity> rarityRepository,
    ITaxonomyRepository<Language> languageRepository
    )
  {
    _contextOptions = contextOptions;
    _treatmentRepository = treatmentRepository;
    _rarityRepository = rarityRepository;
    _languageRepository = languageRepository;
  }

  /// <inheritdoc />
  public async Task UploadCards(IEnumerable<ScryfallCard> cards,
    CancellationToken cancellationToken)
  {
    Guid[] savedCardIds;
    Guid[] savedEditionIds;
    Guid[] savedPrintIds;

    await using (var context = new MagicCollectionContext(_contextOptions))
    {
      savedCardIds = await context.Cards.Select(e => e.Id).ToArrayAsync(cancellationToken);
      savedEditionIds = await context.Editions.Select(e => e.Id).ToArrayAsync(cancellationToken);
      savedPrintIds = await context.Prints.Select(e => e.Id).ToArrayAsync(cancellationToken);
    }

    var scryfallCards = cards as ScryfallCard[] ?? cards.ToArray();

    var cardUploadTasks = scryfallCards
      .DistinctBy(e => e.OracleId)
      .Select(c => CreateCard(c, savedCardIds, cancellationToken));

    var editionUploadTasks = scryfallCards
      .DistinctBy(e => e.SetId)
      .Select(c => CreateEdition(c, savedEditionIds, cancellationToken));

    await Task.WhenAll(cardUploadTasks.Concat(editionUploadTasks));

    var printUploadTasks = scryfallCards
      .Select(c => CreatePrint(c, savedPrintIds, cancellationToken));

    await Task.WhenAll(printUploadTasks);
  }

  private async Task CreateCard(ScryfallCard card, Guid[] savedIds,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    await using var context = new MagicCollectionContext(_contextOptions);

    try
    {
      if (!savedIds.Contains(card.OracleId))
      {
        var cardRecord = new Card
        {
          Id = card.OracleId,
          Name = card.Name
        };

        await context.Cards.AddAsync(cardRecord, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  private async Task CreateEdition(ScryfallCard card, Guid[] savedIds,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    await using var context = new MagicCollectionContext(_contextOptions);

    try
    {
      if (!savedIds.Contains(card.SetId))
      {
        var cardRecord = new Edition
        {
          Id = card.SetId,
          Code = card.Set,
          Name = card.SetName,
          DateReleased = DateOnly.FromDateTime(card.ReleasedAt)
        };

        await context.Editions.AddAsync(cardRecord, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  private async Task CreatePrint(ScryfallCard card, Guid[] savedIds,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    await using var context = new MagicCollectionContext(_contextOptions);

    try
    {
      if (!savedIds.Contains(card.Id))
      {
        var cardRecord = new Print
        {
          Id = card.Id,
          CardId = card.OracleId != Guid.Empty ? card.OracleId : card.CardFaces[0].OracleId,
          EditionId = card.SetId,
          CollectorNumber = card.CollectorNumber,
          DateUpdated = DateTime.UtcNow,
          DefaultLanguage = await _languageRepository.GetOrCreate(context, card.Lang, cancellationToken),
          ScryfallImageUri = GetImageUri(card),
          ScryfallUri = card.ScryfallUri,
          Rarity = await _rarityRepository.GetOrCreate(context, card.Rarity, cancellationToken),
          AvailableTreatments = await GetAvailableTreatments(context, card.Finishes, card.Prices, cancellationToken)
        };

        await context.Prints.AddAsync(cardRecord, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  private async Task<ICollection<PrintTreatment>> GetAvailableTreatments(
    MagicCollectionContext context, string[] finishes, Prices cardPrices, CancellationToken cancellationToken = default)
  {
    var treatments = new List<PrintTreatment>();

    foreach (var f in finishes)
    {
      var treatment = await _treatmentRepository.GetOrCreate(context, f, cancellationToken);
      treatments.Add(new PrintTreatment
      {
        Treatment = treatment,
        Usd = GetTreatmentPrice(cardPrices, f)
      });
    }

    return treatments;
  }

  private decimal? GetTreatmentPrice(Prices cardPrices, string treatment)
  {
    switch (treatment)
    {
      case "nonfoil":
        return decimal.TryParse(cardPrices.Usd, out var usdResult) ? usdResult : null;
      case "foil":
        return decimal.TryParse(cardPrices.UsdFoil, out var usdFoilResult) ? usdFoilResult : null;
      case "etched":
        return decimal.TryParse(cardPrices.UsdEtched, out var usdEtchedResult) ? usdEtchedResult : null;
      default:
        return null;
    }
  }

  private static Uri GetImageUri(ScryfallCard card)
  {
    return card.ImageUris?.Png ?? card.CardFaces[0].ImageUris?.Png;
  }
}
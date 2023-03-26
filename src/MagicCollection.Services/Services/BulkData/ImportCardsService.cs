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

  /// <summary>
  /// 
  /// </summary>
  /// <param name="contextOptions"></param>
  public ImportCardsService(
    DbContextOptions<MagicCollectionContext> contextOptions
  )
  {
    _contextOptions = contextOptions;
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
      .Where(e => !savedCardIds.Contains(e.OracleId))
      .Select(c => CreateCard(c, cancellationToken));

    var editionUploadTasks = scryfallCards
      .DistinctBy(e => e.SetId)
      .Where(e => !savedEditionIds.Contains(e.SetId))
      .Select(c => CreateEdition(c, cancellationToken));

    await Task.WhenAll(cardUploadTasks.Concat(editionUploadTasks));

    var printUploadTasks = scryfallCards
      .Where(e => !savedPrintIds.Contains(e.Id))
      .Select(c => CreatePrint(c, cancellationToken));

    await Task.WhenAll(printUploadTasks);
  }

  private async Task CreateCard(ScryfallCard card, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    await using var context = new MagicCollectionContext(_contextOptions);

    try
    {
      var cardRecord = new Card
      {
        Id = card.OracleId,
        Name = card.Name
      };

      await context.Cards.AddAsync(cardRecord, cancellationToken);
      await context.SaveChangesAsync(cancellationToken);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  private async Task CreateEdition(ScryfallCard card, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    await using var context = new MagicCollectionContext(_contextOptions);

    try
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
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  private async Task CreatePrint(ScryfallCard card, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    await using var context = new MagicCollectionContext(_contextOptions);
    var treatmentRepo = new TaxonomyRepository<Treatment>(context);
    var rarityRepo = new TaxonomyRepository<Rarity>(context);
    var languageRepo = new TaxonomyRepository<Language>(context);

    try
    {
      var cardRecord = new Print
      {
        Id = card.Id,
        CardId = card.OracleId != Guid.Empty ? card.OracleId : card.CardFaces[0].OracleId,
        EditionId = card.SetId,
        CollectorNumber = card.CollectorNumber,
        DateUpdated = DateTime.UtcNow,
        DefaultLanguage = await languageRepo.GetOrCreate(card.Lang, cancellationToken),
        ScryfallImageUri = GetImageUri(card),
        ScryfallUri = card.ScryfallUri,
        Rarity = await rarityRepo.GetOrCreate(card.Rarity, cancellationToken),
        AvailableTreatments = await GetAvailableTreatments(treatmentRepo, card.Finishes, card.Prices, cancellationToken)
      };

      await context.Prints.AddAsync(cardRecord, cancellationToken);
      await context.SaveChangesAsync(cancellationToken);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  private async Task<ICollection<PrintTreatment>> GetAvailableTreatments(
    ITaxonomyRepository<Treatment> treatmentRepository, IEnumerable<string> finishes, Prices cardPrices,
    CancellationToken cancellationToken = default)
  {
    var treatments = new List<PrintTreatment>();

    foreach (var f in finishes)
    {
      var treatment = await treatmentRepository.GetOrCreate(f, cancellationToken);
      treatments.Add(new PrintTreatment
      {
        Treatment = treatment,
        Usd = GetTreatmentPrice(cardPrices, f)
      });
    }

    return treatments;
  }

  private static decimal? GetTreatmentPrice(Prices cardPrices, string treatment)
  {
    return treatment switch
    {
      "nonfoil" => decimal.TryParse(cardPrices.Usd, out var usdResult) ? usdResult : null,
      "foil" => decimal.TryParse(cardPrices.UsdFoil, out var usdFoilResult) ? usdFoilResult : null,
      "etched" => decimal.TryParse(cardPrices.UsdEtched, out var usdEtchedResult) ? usdEtchedResult : null,
      _ => null
    };
  }

  private static Uri GetImageUri(ScryfallCard card)
  {
    return card.ImageUris?.Png ?? card.CardFaces[0].ImageUris?.Png;
  }
}
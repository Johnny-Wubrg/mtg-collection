using MagicCollection.Data;
using MagicCollection.Data.Entities;
using MagicCollection.Data.Entities.Interfaces;
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
    await using var context = new MagicCollectionContext(_contextOptions);

    var savedCardIds = await context.Cards.Select(e => e.Id).ToArrayAsync(cancellationToken);
    var savedEditionIds = await context.Editions.Select(e => e.Id).ToArrayAsync(cancellationToken);
    var savedPrintIds = await context.Prints.Select(e => e.Id).ToArrayAsync(cancellationToken);

    var scryfallCards = cards as ScryfallCard[] ?? cards.ToArray();

    var languages = await PreloadTaxonomy<Language>(
      context,
      scryfallCards.Select(c => c.Lang),
      cancellationToken);

    var rarities = await PreloadTaxonomy<Rarity>(
      context,
      scryfallCards.Select(c => c.Rarity),
      cancellationToken);

    var treatments = await PreloadTaxonomy<Treatment>(
      context,
      scryfallCards.SelectMany(c => c.Finishes),
      cancellationToken);

    var editionTypes = await PreloadTaxonomy<EditionType>(
      context,
      scryfallCards.Select(c => c.SetType),
      cancellationToken);

    await context.SaveChangesAsync(cancellationToken);

    var cardsToAdd = scryfallCards
      .DistinctBy(e => e.OracleId)
      .Where(e => !savedCardIds.Contains(e.OracleId))
      .Select(CreateCard);

    var editionsToAdd = scryfallCards
      .DistinctBy(e => e.SetId)
      .Where(e => !savedEditionIds.Contains(e.SetId))
      .Select(c => CreateEdition(c, editionTypes));

    var printsToAdd = scryfallCards
      .Where(e => !savedPrintIds.Contains(e.Id))
      .Select(c => CreatePrint(c, languages, rarities, treatments));

    await Task.WhenAll([
      context.Cards.AddRangeAsync(cardsToAdd, cancellationToken),
      context.Editions.AddRangeAsync(editionsToAdd, cancellationToken),
      context.Prints.AddRangeAsync(printsToAdd, cancellationToken)
    ]);

    await context.SaveChangesAsync(cancellationToken);

    await UpdatePrints(savedPrintIds, scryfallCards, cancellationToken);
  }


  private Card CreateCard(ScryfallCard card)
  {
    return new Card
    {
      Id = card.OracleId,
      Name = card.Name
    };
  }

  private Edition CreateEdition(ScryfallCard card,
    List<EditionType> editionTypes)
  {
    return new Edition
    {
      Id = card.SetId,
      Code = card.Set,
      Name = card.SetName,
      Type = editionTypes.FirstOrDefault(t => t.Identifier == card.SetType),
      DateReleased = DateOnly.FromDateTime(card.ReleasedAt)
    };
  }

  private async Task<List<TTaxonomy>> PreloadTaxonomy<TTaxonomy>(
    MagicCollectionContext context,
    IEnumerable<string> names,
    CancellationToken cancellationToken)
    where TTaxonomy : class, ITaxonomy, new()
  {
    var repo = new TaxonomyRepository<TTaxonomy>(context);
    var result = (await repo.GetAll(tracked: true)).ToList();

    foreach (var name in names.Distinct().Where(e => result.All(t => t.Identifier != e)))
    {
      result.Add(await repo.GetOrCreate(name, cancellationToken));
    }

    return result;
  }

  private Print CreatePrint(ScryfallCard card, List<Language> languages, List<Rarity> rarities,
    List<Treatment> treatments)
  {
    return new Print
    {
      Id = card.Id,
      CardId = card.OracleId != Guid.Empty ? card.OracleId : card.CardFaces[0].OracleId,
      EditionId = card.SetId,
      CollectorNumber = card.CollectorNumber,
      DateUpdated = DateTime.UtcNow,
      DefaultLanguage = languages.FirstOrDefault(l => l.Identifier == card.Lang),
      ScryfallImageUri = GetImageUri(card),
      ScryfallUri = card.ScryfallUri,
      Rarity = rarities.FirstOrDefault(r => r.Identifier == card.Rarity),
      AvailableTreatments = GetAvailableTreatments(treatments, card.Finishes, card.Prices)
    };
  }


  private async Task UpdatePrints(Guid[] savedPrintIds, ScryfallCard[] scryfallCards,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    await using var context = new MagicCollectionContext(_contextOptions);

    var printsToUpdate = context.Prints
      .Include(e => e.AvailableTreatments)
      .Where(e => savedPrintIds.Contains(e.Id));

    foreach (var print in printsToUpdate)
    {
      var card = scryfallCards.FirstOrDefault(e => e.Id == print.Id);
      UpdatePrint(print, card);
    }

    await context.SaveChangesAsync(cancellationToken);
  }

  private static void UpdatePrint(Print print, ScryfallCard card)
  {
    print.DateUpdated = DateTime.UtcNow;

    if (card is null)
    {
      print.ScryfallDeleted = true;
      return;
    }

    foreach (var treatment in print.AvailableTreatments)
    {
      treatment.Usd = GetTreatmentPrice(card.Prices, treatment.TreatmentId);
    }
    
    print.EditionId = card.SetId;
    print.CollectorNumber = card.CollectorNumber;
  }

  private ICollection<PrintTreatment> GetAvailableTreatments(
    IEnumerable<Treatment> treatments, IEnumerable<string> finishes, Prices cardPrices)
  {
    var result = new List<PrintTreatment>();

    foreach (var f in finishes)
    {
      var treatment = treatments.FirstOrDefault(t => t.Identifier == f);
      result.Add(new PrintTreatment
      {
        Treatment = treatment,
        Usd = GetTreatmentPrice(cardPrices, f)
      });
    }

    return result;
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

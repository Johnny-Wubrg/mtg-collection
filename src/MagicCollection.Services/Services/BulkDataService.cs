using MagicCollection.Data;
using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ScryNet.Client;
using ScryNet.Models;

namespace MagicCollection.Services;

/// <inheritdoc />
public class BulkDataService : IBulkDataService
{
  private readonly DbContextOptions<MagicCollectionContext> _contextOptions;
  private readonly IScryfallClient _client;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="contextOptions"></param>
  /// <param name="client"></param>
  public BulkDataService(DbContextOptions<MagicCollectionContext> contextOptions, IScryfallClient client)
  {
    _contextOptions = contextOptions;
    _client = client;
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
          CardId = card.OracleId,
          EditionId = card.SetId,
          CollectorNumber = card.CollectorNumber,
          DateUpdated = DateTime.UtcNow,
          Language = await GetLanguage(context, card.Lang),
          ScryfallImageUri = GetImageUri(card),
          ScryfallUri = card.ScryfallUri,
          Rarity = await GetRarity(context, card.Rarity),
          Prices = await GetPrices(context, card.Prices),
          AvailableTreatments = await GetAvailableTreatments(context, card.Finishes)
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

  private async Task<Language> GetLanguage(MagicCollectionContext context, string id)
  {
    var found = await context.Languages.FirstOrDefaultAsync(l => l.Identifier == id);
    if (found is not null) return found;

    var newLang = new Language
    {
      Identifier = id,
      Label = id
    };

    context.Languages.Add(newLang);

    return newLang;
  }

  private async Task<Rarity> GetRarity(MagicCollectionContext context, string id)
  {
    var found = await context.Rarities.FirstOrDefaultAsync(l => l.Identifier == id);
    if (found is not null) return found;

    var newRarity = new Rarity
    {
      Identifier = id,
      Label = id
    };

    context.Rarities.Add(newRarity);

    return newRarity;
  }

  private async Task<ICollection<Price>> GetPrices(
    MagicCollectionContext context,
    Prices cardPrices
    )
  {
    var prices = new List<Price>();

    if (!string.IsNullOrEmpty(cardPrices.Usd)) prices.Add(new Price
    {
      Treatment = await GetTreatment(context, "nonfoil"),
      Amount = decimal.Parse(cardPrices.Usd)
    });

    if (!string.IsNullOrEmpty(cardPrices.UsdFoil)) prices.Add(new Price
    {
      Treatment = await GetTreatment(context, "foil"),
      Amount = decimal.Parse(cardPrices.UsdFoil)
    });

    if (!string.IsNullOrEmpty(cardPrices.UsdEtched)) prices.Add(new Price
    {
      Treatment = await GetTreatment(context, "etched"),
      Amount = decimal.Parse(cardPrices.UsdEtched)
    });

    return prices;
  }

  private async Task<ICollection<Treatment>> GetAvailableTreatments(
    MagicCollectionContext context, string[] finishes)
  {
    var treatments = new List<Treatment>();

    foreach (var f in finishes)
    {
      treatments.Add(await GetTreatment(context, f));
    }

    return treatments;
  }

  private async Task<Treatment> GetTreatment(MagicCollectionContext context, string id)
  {
    var found = await context.Treatments.FirstOrDefaultAsync(l => l.Identifier == id);
    if (found is not null) return found;

    var newTreatment = new Treatment
    {
      Identifier = id,
      Label = id
    };

    context.Treatments.Add(newTreatment);

    return newTreatment;
  }

  private static Uri GetImageUri(ScryfallCard card)
  {
    return card.ImageUris?.Png ?? card.CardFaces[0].ImageUris?.Png;
  }
}
using ScryNet.Models;

namespace MagicCollection.Services.BulkData;

/// <summary>
/// Service to handle bulk card data imports
/// </summary>
public interface IImportCardsService
{
  /// <summary>
  /// Upload multiple card definitions from Scryfall
  /// </summary>
  /// <param name="cards"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  Task UploadCards(IEnumerable<ScryfallCard> cards, CancellationToken cancellationToken);
}
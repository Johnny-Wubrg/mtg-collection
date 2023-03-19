using ScryNet.Models;

namespace MagicCollection.Services;

/// <summary>
/// Service to handle bulk data loads.
/// </summary>
public interface IBulkDataService
{
  /// <summary>
  /// Upload multiple card definitions from Scryfall
  /// </summary>
  /// <param name="cards"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  Task UploadCards(IEnumerable<ScryfallCard> cards, CancellationToken cancellationToken);
}
using ScryNet.Models;

namespace MagicCollection.Services.BulkData;

/// <summary>
/// Service to handle importing collection.
/// </summary>
public interface IImportCollectionService
{
  /// <summary>
  /// Upload multiple card definitions from Scryfall
  /// </summary>
  /// <param name="entries"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  Task UploadCollection(
    IEnumerable<Dictionary<string, string>> entries,
    CancellationToken cancellationToken
  );
}
using MagicCollection.Services.Models.Collection;
using MagicCollection.Services.Models.Request;

namespace MagicCollection.Services.Collection;

/// <summary>
/// Service to interact with collection entries
/// </summary>
public interface ICardEntryService
{
  /// <summary>
  /// Adds a quantity of cards to an entry to a collection
  /// </summary>
  /// <param name="model"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  Task Add(CardEntryRequestModel model, CancellationToken cancellationToken = default);

  /// <summary>
  /// Bulk add cards to a collection
  /// </summary>
  /// <param name="models"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  Task AddRange(IEnumerable<CardEntryRequestModel> models, CancellationToken cancellationToken = default);
}
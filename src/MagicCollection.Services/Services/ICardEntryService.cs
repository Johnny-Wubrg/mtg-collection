using MagicCollection.Services.Models.Collection;

namespace MagicCollection.Services;

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
  Task Add(CardEntryModel model, CancellationToken cancellationToken = default);
}
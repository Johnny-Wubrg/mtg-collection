using MagicCollection.Services.Models;

namespace MagicCollection.Services;

/// <summary>
/// Service to interact with card data
/// </summary>
public interface ICardService
{
  /// <summary>
  /// Get all available cards
  /// </summary>
  /// <returns></returns>
  Task<IEnumerable<CardModel>> GetAll();

  /// <summary>
  /// Get a card by its Scryfall oracle ID
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<CardModel> Get(Guid id);

  /// <summary>
  /// Search for cards by name
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  Task<IEnumerable<CardModel>> Search(string name);
}
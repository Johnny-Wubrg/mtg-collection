using MagicCollection.Services.Models.Interfaces;

namespace MagicCollection.Services.Cards;

/// <summary>
/// Service to manage taxonomy
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface ITaxonomyService<TModel> where TModel : class, ITaxonomyModel, new()
{
  /// <summary>
  /// Get all taxonomy.
  /// </summary>
  /// <returns></returns>
  Task<IEnumerable<TModel>> GetAll();
}
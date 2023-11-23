using MagicCollection.Services.Models.Collection;

namespace MagicCollection.Services.Collection;

/// <summary>
/// Service to interact with bin data
/// </summary>
public interface IBinService
{
  /// <summary>
  /// Get all bins
  /// </summary>
  /// <returns></returns>
  Task<IEnumerable<BinModel>> GetAll();

  
  /// <summary>
  /// Get one bin by id
  /// </summary>
  /// <returns></returns>
  Task<BinModel> Get(Guid id);
}
using MagicCollection.Services.Models.Collection;

namespace MagicCollection.Services.Collection;

/// <summary>
/// Service to interact with section data
/// </summary>
public interface ISectionService
{
  /// <summary>
  /// Get all sections in a bin
  /// </summary>
  /// <param name="binId"></param>
  /// <returns></returns>
  Task<IEnumerable<SectionModel>> GetAllByBin(Guid binId);
  
  /// <summary>
  /// Get a section in a bin by its id
  /// </summary>
  /// <param name="binId"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<SectionModel> GetByBin(Guid binId, Guid id);
}
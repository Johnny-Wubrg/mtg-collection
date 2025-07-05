using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicCollection.Services.Models.Collection;

/// <summary>
/// A storage location
/// </summary>
public class BinModel
{
  /// <summary>
  /// The internal ID of the storage location
  /// </summary>
  /// <example>63c96b55-4717-4e8c-a7e6-5cc00a8f08ad</example>
  public Guid Id { get; set; }
  
  /// <summary>
  /// The display name of the storage location
  /// </summary>
  /// <example>Big Bin</example>
  [Required]
  public string Label { get; set; }
  
  
  /// <summary>
  /// Sections within the storage location
  /// </summary>
  public IEnumerable<SectionModel> Sections { get; set; }
}
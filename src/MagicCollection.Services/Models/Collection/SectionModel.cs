using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Services.Models.Collection;

/// <summary>
/// A subdivision of a storage location
/// </summary>
public class SectionModel
{
  /// <summary>
  /// The internal ID of the subdivision
  /// </summary>
  /// <example>4fc8e55a-f4e4-4b33-9843-2a6ed6327beb</example>
  public Guid Id { get; set; }
  
  /// <summary>
  /// The display name of the subdivision
  /// </summary>
  /// <example>Artifacts in the Big Bin</example>
  [Required]
  public string Label { get; set; }
  
  /// <summary>
  /// The order value of the subdivision
  /// </summary>
  /// <example>1</example>
  public int Ordinal { get; set; }
}
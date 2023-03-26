namespace MagicCollection.Services.Models.Interfaces;

/// <summary>
/// Interface for a taxonomy record
/// </summary>
public interface ITaxonomyModel
{
  /// <summary>
  /// The identifier of the taxonomy
  /// </summary>
  string Identifier { get; set; }
  
  /// <summary>
  /// The display value of the taxonomy
  /// </summary>
  string Label { get; set; }
}
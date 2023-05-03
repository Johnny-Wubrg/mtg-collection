namespace MagicCollection.Services.Models.Request;

/// <summary>
/// Model to create a card entry record
/// </summary>
public record CardEntryRequestModel
{
  /// <summary>
  /// Id of the print to add
  /// </summary>
  /// <example>036ef8c9-72ac-46ce-af07-83b79d736538</example>
  public Guid Print { get; set; }
  
  /// <summary>
  /// Identifier of a treatment for the entry
  /// </summary>
  /// <example>nonfoil</example>
  public string Treatment { get; set; }
  
  /// <summary>
  /// Identifier of a language for the entry
  /// </summary>
  /// <example>en</example>
  public string Language { get; set; }
  
  /// <summary>
  /// Number of cards to add to the entry
  /// </summary>
  /// <example>1</example>
  public int Quantity { get; set; }
  
  /// <summary>
  /// Id of a section to add to
  /// </summary>
  /// <example>4fc8e55a-f4e4-4b33-9843-2a6ed6327beb</example>
  public Guid? Section { get; set; }
};
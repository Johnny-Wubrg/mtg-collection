namespace MagicCollection.Services.Models.Request;

/// <summary>
/// Model to search for prints against
/// </summary>
public record PrintSearchModel
{
  /// <summary>
  /// Name of a card to search for
  /// </summary>
  /// <example>storm</example>
  public string Name { get; set; }
  
  /// <summary>
  /// Set code to filter against
  /// </summary>
  /// <example>9ed</example>
  public string Set { get; set; }
  
  /// <summary>
  /// Collector Number to search for
  /// </summary>
  /// <example>100</example>
  public string CollectorNumber { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Services.Models.Cards;

/// <summary>
/// A Magic: the Gathering card set
/// </summary>
public class EditionModel
{
  /// <summary>
  /// The set's Scryfall ID
  /// </summary>
  /// <example>e70c8572-4732-4e92-a140-b4e3c1c84c93</example>
  public Guid Id { get; set; }

  /// <summary>
  /// The set code
  /// </summary>
  /// <example>9ed</example>
  [Required]
  public string Code { get; set; }

  /// <summary>
  /// The name of the set
  /// </summary>
  /// <example>Ninth Edition</example>
  [Required]
  public string Name { get; set; }
  
  /// <summary>
  /// A classification for the set
  /// </summary>
  [Required]
  public EditionTypeModel Type { get; set; }

  /// <summary>
  /// The date the set was released 
  /// </summary>
  /// <example>2005-07-29</example>
  public DateOnly DateReleased { get; set; }
}
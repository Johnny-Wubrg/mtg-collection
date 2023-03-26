using System.ComponentModel.DataAnnotations;
using MagicCollection.Services.Models.Interfaces;

namespace MagicCollection.Services.Models.Cards;

/// <summary>
/// A language printed on a Magic: the Gathering card
/// </summary>
public class EditionTypeModel : ITaxonomyModel
{
  /// <summary>
  /// The identifier Scryfall uses for a set type
  /// </summary>
  /// <example>core</example>
  [Required]
  public string Identifier { get; set; }
  
  /// <summary>
  /// The name of the set type
  /// </summary>
  /// <example>Core</example>
  [Required]
  public string Label { get; set; }
}
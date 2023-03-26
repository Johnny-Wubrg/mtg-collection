using System.ComponentModel.DataAnnotations;
using MagicCollection.Services.Models.Interfaces;

namespace MagicCollection.Services.Models.Cards;

/// <summary>
/// A card's printed rarity
/// </summary>
public class RarityModel : ITaxonomyModel
{
  /// <summary>
  /// The identifier Scryfall uses for a rarity
  /// </summary>
  /// <example>common</example>
  [Required]
  public string Identifier { get; set; }
  
  /// <summary>
  /// The display name of the rarity
  /// </summary>
  /// <example>Common</example>
  [Required]
  public string Label { get; set; }
}
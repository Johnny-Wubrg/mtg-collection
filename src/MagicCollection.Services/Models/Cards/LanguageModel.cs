using System.ComponentModel.DataAnnotations;
using MagicCollection.Services.Models.Interfaces;

namespace MagicCollection.Services.Models.Cards;

/// <summary>
/// A language printed on a Magic: the Gathering card
/// </summary>
public class LanguageModel : ITaxonomyModel
{
  /// <summary>
  /// The identifier Scryfall uses for a language
  /// </summary>
  /// <example>en</example>
  [Required]
  public string Identifier { get; set; }
  
  /// <summary>
  /// The name of the language
  /// </summary>
  /// <example>English</example>
  [Required]
  public string Label { get; set; }
}
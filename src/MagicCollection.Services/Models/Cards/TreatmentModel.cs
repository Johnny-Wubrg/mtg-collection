using System.ComponentModel.DataAnnotations;
using MagicCollection.Services.Models.Interfaces;

namespace MagicCollection.Services.Models.Cards;

/// <summary>
/// Foiling treatments used on a card.
/// </summary>
public class TreatmentModel : ITaxonomyModel
{
  /// <summary>
  /// The identifier Scryfall uses for a treatment
  /// </summary>
  /// <example>nonfoil</example>
  [Required]
  public string Identifier { get; set; }
  
  
  /// <summary>
  /// The display name of the treatment
  /// </summary>
  /// <example>Nonfoil</example>
  [Required]
  public string Label { get; set; }
}
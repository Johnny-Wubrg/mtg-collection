using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Services.Models.Cards;

/// <summary>
/// A Magic: the Gathering card
/// </summary>
public class CardModel
{
  /// <summary>
  /// The card's oracle ID from Scryfall
  /// </summary>
  /// <example>000d5588-5a4c-434e-988d-396632ade42c</example>
  public Guid Id { get; set; }

  /// <summary>
  /// The oracle name of the card
  /// </summary>
  /// <example>Storm Crow</example>
  [Required]
  public string Name { get; set; }
}
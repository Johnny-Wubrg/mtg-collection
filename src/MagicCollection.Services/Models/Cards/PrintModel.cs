using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Services.Models.Cards;

/// <summary>
/// A specific printing of a Magic: the Gathering card
/// </summary>
public class PrintModel
{
  /// <summary>
  /// The Scryfall ID of the printing
  /// </summary>
  /// <example>036ef8c9-72ac-46ce-af07-83b79d736538</example>
  public Guid Id { get; set; }

  /// <summary>
  /// The related card data of the printing
  /// </summary>
  public CardModel Card { get; set; }

  /// <summary>
  /// The set or edition the card was printed in
  /// </summary>
  public EditionModel Edition { get; set; }

  /// <summary>
  /// The card's collector's number
  /// </summary>
  /// <example>100</example>
  [Required]
  public string CollectorNumber { get; set; }

  /// <summary>
  /// The card's printed rarity
  /// </summary>
  public RarityModel Rarity { get; set; }

  /// <summary>
  /// The default language of the card
  /// </summary>
  public LanguageModel DefaultLanguage { get; set; }

  /// <summary>
  /// The foiling treatments the card is available in.
  /// </summary>
  [Required]
  public ICollection<TreatmentModel> AvailableTreatments { get; set; }

  
  /// <summary>
  /// URI to the card's Scryfall page
  /// </summary>
  /// <example>https://scryfall.com/card/9ed/100/storm-crow?utm_source=api</example>
  [Required]
  public Uri ScryfallUri { get; set; }

  /// <summary>
  /// URI to card image as provided by Scryfall
  /// </summary>
  /// <example>https://cards.scryfall.io/png/front/0/3/036ef8c9-72ac-46ce-af07-83b79d736538.png?1562730661</example>
  [Required]
  public Uri ScryfallImageUri { get; set; }
}
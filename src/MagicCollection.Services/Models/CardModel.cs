namespace MagicCollection.Services.Models;

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
  public string Name { get; set; }
  
  /// <summary>
  /// URI to the card's Scryfall page
  /// </summary>
  /// <example>https://scryfall.com/card/9ed/100/storm-crow?utm_source=api</example>
  public Uri ScryfallUri { get; set; }

  /// <summary>
  /// URI to card image as provided by Scryfall
  /// </summary>
  /// <example>https://cards.scryfall.io/png/front/0/3/036ef8c9-72ac-46ce-af07-83b79d736538.png?1562730661</example>
  public Uri ScryfallImageUri { get; set; }
}
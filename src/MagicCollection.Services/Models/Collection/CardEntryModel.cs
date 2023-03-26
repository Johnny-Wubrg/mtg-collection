using System.ComponentModel.DataAnnotations;
using MagicCollection.Services.Models.Cards;

namespace MagicCollection.Services.Models.Collection;

/// <summary>
/// A unique card entry in a collection
/// </summary>
public class CardEntryModel
{
  /// <summary>
  /// The internal ID of the entry
  /// </summary>
  public Guid Id { get; set; }

  /// <summary>
  /// The number of cards for this entry
  /// </summary>
  [Required]
  public int Quantity { get; set; }

  /// <summary>
  /// The printing that is represented by the entry
  /// </summary>
  public PrintModel Print { get; set; }

  /// <summary>
  /// The foiling treatment that is represented bu the entry
  /// </summary>
  [Required]
  public TreatmentModel Treatment { get; set; }

  /// <summary>
  /// The language that is represented bu the entry
  /// </summary>
  [Required]
  public LanguageModel Language { get; set; }

  /// <summary>
  /// The subdivision this card is stored in
  /// </summary>
  [Required]
  public SectionModel Section { get; set; }
}
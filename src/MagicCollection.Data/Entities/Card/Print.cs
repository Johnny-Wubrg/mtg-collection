using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicCollection.Data.Entities;

public class Print
{
  [Key]
  public Guid Id { get; set; }

  public Guid CardId { get; set; }

  [ForeignKey(nameof(CardId))]
  public Card Card { get; set; }

  public Guid EditionId { get; set; }
  
  [ForeignKey(nameof(EditionId))]
  public Edition Edition { get; set; }

  [MaxLength(8)]
  public string CollectorNumber { get; set; }

  public Rarity Rarity { get; set; }
  public Language DefaultLanguage { get; set; }
  public ICollection<PrintTreatment> AvailableTreatments { get; set; }

  [Required, MaxLength(256)]
  public Uri ScryfallUri { get; set; }

  [MaxLength(256)]
  public Uri ScryfallImageUri { get; set; }

  public DateTime DateUpdated { get; set; }
}
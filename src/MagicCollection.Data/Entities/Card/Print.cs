using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Data.Entities;

public class Print
{
  [Key]
  public Guid Id { get; set; }

  public Card Card { get; set; }
  public Edition Edition { get; set; }

  [MaxLength(8)]
  public string CollectorNumber { get; set; }

  public decimal Usd { get; set; }
  public Rarity Rarity { get; set; }
  public Language Language { get; set; }
  public ICollection<Treatment> AvailableTreatments { get; set; }

  [Required, MaxLength(256)]
  public Uri ScryfallUri { get; set; }

  [MaxLength(256)]
  public Uri ScryfallImageUri { get; set; }

  public DateTime DateUpdated { get; set; }
}
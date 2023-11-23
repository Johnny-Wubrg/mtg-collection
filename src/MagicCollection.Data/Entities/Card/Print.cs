using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Entities;

public class Print : IEntity
{
  [Key]
  public Guid Id { get; set; }

  public Guid CardId { get; set; }

  [ForeignKey(nameof(CardId))]
  public Card Card { get; set; }

  public Guid EditionId { get; set; }
  
  [ForeignKey(nameof(EditionId))]
  public Edition Edition { get; set; }

  [MaxLength(16)]
  public string CollectorNumber { get; set; }

  [Required]
  public Rarity Rarity { get; set; }

  [Required]
  public Language DefaultLanguage { get; set; }
  
  public ICollection<PrintTreatment> AvailableTreatments { get; set; }

  [Required, MaxLength(256)]
  public Uri ScryfallUri { get; set; }

  [MaxLength(256)]
  public Uri ScryfallImageUri { get; set; }

  public DateTime DateUpdated { get; set; }
  
  public bool ScryfallDeleted { get; set; }
}
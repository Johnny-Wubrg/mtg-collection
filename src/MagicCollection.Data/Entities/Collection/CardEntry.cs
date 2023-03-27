using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MagicCollection.Data.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Entities;

[Index(nameof(PrintId), nameof(TreatmentIdentifier), nameof(LanguageIdentifier), nameof(SectionId), IsUnique = true)]
public class CardEntry : IEntity
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  public int Quantity { get; set; }

  [Required]
  public Guid PrintId { get; set; }

  [Required, ForeignKey(nameof(PrintId))]
  public Print Print { get; set; }

  [Required]
  public string TreatmentIdentifier { get; set; }

  [Required, ForeignKey(nameof(TreatmentIdentifier))]
  public Treatment Treatment { get; set; }

  [Required]
  public string LanguageIdentifier { get; set; }

  [Required, ForeignKey(nameof(LanguageIdentifier))]
  public Language Language { get; set; }

  [Required]
  public Guid SectionId { get; set; }

  [Required, ForeignKey(nameof(SectionId))]
  public Section Section { get; set; }
}
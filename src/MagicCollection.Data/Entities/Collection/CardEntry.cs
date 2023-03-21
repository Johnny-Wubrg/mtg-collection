using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicCollection.Data.Entities;

public class CardEntry
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  public int Quantity { get; set; }
  
  [Required]
  public Print Print { get; set; }

  [Required]
  public string TreatmentIdentifier { get; set; }
  
  [Required, ForeignKey(nameof(TreatmentIdentifier))]
  public Treatment Treatment { get; set; }

  [Required]
  public string LanguageIdentifier { get; set; }
  
  [Required, ForeignKey(nameof(LanguageIdentifier))]
  public Language Language { get; set; }
}
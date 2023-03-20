using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicCollection.Data.Entities;

public class CardEntry
{
  [Key]
  public Guid Id { get; set; }

  public int Quantity { get; set; }
  
  public Print Print { get; set; }

  public string TreatmentIdentifier { get; set; }
  
  [ForeignKey(nameof(TreatmentIdentifier))]
  public Treatment Treatment { get; set; }

  public string LanguageIdentifier { get; set; }
  
  [ForeignKey(nameof(LanguageIdentifier))]
  public Language Language { get; set; }
}
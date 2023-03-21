using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Data.Entities;

public class Section
{
  [Key]
  public Guid Id { get; set; }
  
  [MaxLength(64)]
  public string Label { get; set; }

  public Bin Bin { get; set; }
  
  public int Ordinal { get; set; }
}
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Data.Entities;

public class Bin
{
  [Key]
  public Guid Id { get; set; }
  
  [MaxLength(64)]
  public string Label { get; set; }
  
  public ICollection<Section> Sections { get; set; }
}
using System.ComponentModel.DataAnnotations;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Entities;

public class Bin : IEntity
{
  [Key]
  public Guid Id { get; set; }
  
  [MaxLength(64)]
  public string Label { get; set; }
  
  public ICollection<Section> Sections { get; set; }

  public int Ordinal { get; set; }
}
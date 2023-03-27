using System.ComponentModel.DataAnnotations;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Entities;

public class Edition : IEntity
{
  [Key]
  public Guid Id { get; set; }
  
  [MaxLength(8)]
  public string Code { get; set; }
  
  [MaxLength(64)]
  public string Name { get; set; }
  
  public EditionType Type { get; set; }
  
  public DateOnly DateReleased { get; set; }
}
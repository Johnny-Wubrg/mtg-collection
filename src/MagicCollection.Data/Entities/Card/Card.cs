using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Data.Entities;

public class Card
{
  [Key]
  public Guid Id { get; set; }
  
  [Required, MaxLength(256)]
  public string Name { get; set; }
}
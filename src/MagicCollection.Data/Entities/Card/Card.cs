using System.ComponentModel.DataAnnotations;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Entities;

public class Card : IEntity
{
  [Key]
  public Guid Id { get; set; }
  
  [Required, MaxLength(256)]
  public string Name { get; set; }
}
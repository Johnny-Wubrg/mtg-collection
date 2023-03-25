using System.ComponentModel.DataAnnotations;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Entities;

public class Rarity : ITaxonomy
{
  [Key, MaxLength(16)]
  public string Identifier { get; set; }
  
  [MaxLength(16)]
  public string Label { get; set; }

  public int Ordinal { get; set; }
}
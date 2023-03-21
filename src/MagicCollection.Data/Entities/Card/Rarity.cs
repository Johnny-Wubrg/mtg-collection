using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Data.Entities;

public class Rarity
{
  [Key, MaxLength(16)]
  public string Identifier { get; set; }
  
  [MaxLength(16)]
  public string Label { get; set; }

  public int Ordinal { get; set; }
}
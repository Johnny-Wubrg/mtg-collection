using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Data.Entities;

public class Treatment
{
  [Key, MaxLength(16)]
  public string Identifier { get; set; }

  [MaxLength(32)]
  public string Label { get; set; }
}
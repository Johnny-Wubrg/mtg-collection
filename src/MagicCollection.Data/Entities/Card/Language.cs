using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Data.Entities;

public class Language
{
  [Key, MaxLength(4)]
  public string Identifier { get; set; }

  [MaxLength(32)]
  public string Label { get; set; }
}
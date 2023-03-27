using System.ComponentModel.DataAnnotations;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Entities;

public class Language : ITaxonomy
{
  [Key, MaxLength(4)]
  public string Identifier { get; set; }

  [MaxLength(32)]
  public string Label { get; set; }

  public int Ordinal { get; set; }
}
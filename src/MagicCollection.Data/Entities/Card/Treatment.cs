using System.ComponentModel.DataAnnotations;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Entities;

public class Treatment : ITaxonomy
{
  [Key, MaxLength(16)]
  public string Identifier { get; set; }

  [MaxLength(32)]
  public string Label { get; set; }

  public int Ordinal { get; set; }
}
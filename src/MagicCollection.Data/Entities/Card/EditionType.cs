using System.ComponentModel.DataAnnotations;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Entities;

public class EditionType : ITaxonomy
{
  [Key, MaxLength(24)]
  public string Identifier { get; set; }

  [MaxLength(24)]
  public string Label { get; set; }
}
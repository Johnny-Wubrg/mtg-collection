namespace MagicCollection.Data.Entities.Interfaces;

public interface ITaxonomy
{
  string Identifier { get; set; }
  string Label { get; set; }
  int Ordinal { get; set; }
}
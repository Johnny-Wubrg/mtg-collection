using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Data.Entities;

public class CardEntry
{
  [Key]
  public Guid Id { get; set; }

  public int Quantity { get; set; }
  public Print Print { get; set; }
  public Treatment Treatment { get; set; }
}
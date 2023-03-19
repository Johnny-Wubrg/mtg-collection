using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Entities
{
  [PrimaryKey(nameof(PrintId), nameof(TreatmentId))]
  public class Price
  {
    public Guid PrintId { get; set; }
    [ForeignKey(nameof(PrintId))]
    public Print Print { get; set; }
    public string TreatmentId { get; set; }
    [ForeignKey(nameof(TreatmentId))]
    public Treatment Treatment { get; set; }
    public decimal Amount { get; set; }
  }
}
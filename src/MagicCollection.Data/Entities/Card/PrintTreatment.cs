using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Entities
{
  [PrimaryKey(nameof(PrintId), nameof(TreatmentId))]
  public class PrintTreatment
  {
    public Guid PrintId { get; set; }
    [ForeignKey(nameof(PrintId))]
    public Print Print { get; set; }
    public string TreatmentId { get; set; }
    [ForeignKey(nameof(TreatmentId))]
    public Treatment Treatment { get; set; }

    [Column(TypeName = "money")]
    public decimal? Usd { get; set; }
  }
}
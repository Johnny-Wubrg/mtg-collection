using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public class PrintRepository : EntityRepository<Print>, IPrintRepository
{
  public PrintRepository(MagicCollectionContext context) : base(context)
  {
  }

  protected override IQueryable<Print> Includer(IQueryable<Print> query) =>
    query
      .Include(p => p.Card)
      .Include(p => p.Edition)
        .ThenInclude(e => e.Type)
      .Include(p => p.AvailableTreatments.OrderBy(t => t.Treatment.Ordinal))
        .ThenInclude(e => e.Treatment)
      .Include(p => p.DefaultLanguage)
      .Include(p => p.Rarity);

  protected override IQueryable<Print> DefaultTransform(IQueryable<Print> query)
  {
    return query.OrderByDescending(p => p.Edition.DateReleased);
  }
}
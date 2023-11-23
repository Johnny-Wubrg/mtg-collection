using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories.Collection;

public class BinRepository : EntityRepository<Bin>, IBinRepository
{
  public BinRepository(MagicCollectionContext context) : base(context)
  {
  }

  public async Task<Bin> GetOrCreate(string label, CancellationToken cancellationToken = default)
  {
    var found = await Context.Bins.FirstOrDefaultAsync(e => e.Label == label, cancellationToken: cancellationToken);
    if (found is not null) return found;

    var newRecord = new Bin { Label = label };
    await Context.Bins.AddAsync(newRecord, cancellationToken);

    return newRecord;
  }

  protected override IQueryable<Bin> Includer(IQueryable<Bin> query)
  {
    return query.Include(e => e.Sections);
  }
}
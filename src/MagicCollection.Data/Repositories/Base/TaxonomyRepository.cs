using MagicCollection.Data.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public class TaxonomyRepository<T> : Repository<T>, ITaxonomyRepository<T> where T : class, ITaxonomy, new()
{
  public TaxonomyRepository(MagicCollectionContext context) : base(context)
  {
  }

  public async Task<T> GetOrCreate(string id, CancellationToken cancellationToken = default)
  {
    if (string.IsNullOrWhiteSpace(id)) return null;

    var found = await Context.Set<T>()
      .FirstOrDefaultAsync(l => l.Identifier == id, cancellationToken: cancellationToken);
    if (found is not null) return found;

    var newRecord = new T
    {
      Identifier = id,
      Label = id
    };

    await Context.Set<T>().AddAsync(newRecord, cancellationToken);

    return newRecord;
  }

  protected override IQueryable<T> DefaultTransform(IQueryable<T> query) =>
    query.OrderBy(e => e.Ordinal).ThenBy(e => e.Label);
}
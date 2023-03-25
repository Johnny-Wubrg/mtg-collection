using MagicCollection.Data.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public class TaxonomyRepository<T> : ITaxonomyRepository<T> where T : class, ITaxonomy, new()
{
  private readonly MagicCollectionContext _context;

  public TaxonomyRepository(MagicCollectionContext context)
  {
    _context = context;
  }

  public async Task<T> GetOrCreate(string id, CancellationToken cancellationToken = default)
  {
    return await GetOrCreate(_context, id, cancellationToken);
  }

  public async Task<T> GetOrCreate(MagicCollectionContext context, string id,
    CancellationToken cancellationToken = default)
  {
    if (string.IsNullOrWhiteSpace(id)) return null;

    var found = await context.Set<T>()
      .FirstOrDefaultAsync(l => l.Identifier == id, cancellationToken: cancellationToken);
    if (found is not null) return found;

    var newRecord = new T
    {
      Identifier = id,
      Label = id
    };

    await context.Set<T>().AddAsync(newRecord, cancellationToken);

    return newRecord;
  }
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class, new()
{
  protected readonly MagicCollectionContext Context;

  protected Repository(MagicCollectionContext context)
  {
    Context = context;
  }

  public async Task<IEnumerable<T>> GetAll(
    Func<IQueryable<T>, IQueryable<T>> transform = null,
    bool tracked = false
  )
  {
    var query = Includer(Context.Set<T>());
    query = transform == null ? DefaultTransform(query) : transform(query);
    if (!tracked) query = query.AsNoTracking();
    return await query.ToListAsync();
  }
  
  public async Task<T> Find(Expression<Func<T, bool>> predicate) =>
    await Includer(Context.Set<T>()).FirstOrDefaultAsync(predicate);

  public async Task SaveChanges()
  {
    await Context.SaveChangesAsync();
  }

  protected virtual IQueryable<T> Includer(IQueryable<T> query) => query;

  protected virtual IQueryable<T> DefaultTransform(IQueryable<T> query) => query;
}
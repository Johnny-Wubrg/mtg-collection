using System.Linq.Expressions;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Repositories;

public interface IRepository<T> where T : class, new()
{
  public Task<IEnumerable<T>> GetAll(
    Func<IQueryable<T>, IQueryable<T>> transform = null,
    bool tracked = false
  );

  public Task SaveChanges();

  public Task<T> Find(Expression<Func<T, bool>> predicate);
}
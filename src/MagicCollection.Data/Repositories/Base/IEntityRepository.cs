using System.Linq.Expressions;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Repositories;

public interface IEntityRepository<T> where T : class, IEntity, new()
{
  Task<T> Get(Guid id);
  Task<T> GetUntracked(Guid id);

  Task<IEnumerable<T>> GetAll(
    Func<IQueryable<T>, IQueryable<T>> transform = null,
    bool tracked = false
  );

  Task<T> Find(Expression<Func<T, bool>> predicate);
  Task SaveChanges();
}
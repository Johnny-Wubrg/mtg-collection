using System.Linq.Expressions;
using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Repositories;

public interface IEntityRepository<T> : IRepository<T> where T : class, IEntity, new()
{
  Task<T> Get(Guid id);
  Task<T> GetUntracked(Guid id);
}
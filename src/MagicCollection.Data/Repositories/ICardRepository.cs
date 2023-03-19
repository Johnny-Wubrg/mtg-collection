using System.Linq.Expressions;
using MagicCollection.Data.Entities;

namespace MagicCollection.Data.Repositories;

public interface ICardRepository
{
  Task<IEnumerable<Card>> GetAll(
    Func<IQueryable<Card>, IQueryable<Card>> transform = null,
    bool tracked = false
  );

  Task<Card> Get(Guid id);
  Task<Card> GetUntracked(Guid id);
  Task<Card> Find(Expression<Func<Card, bool>> predicate);
  Task Commit();
}
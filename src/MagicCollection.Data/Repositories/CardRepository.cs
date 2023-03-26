using System.Linq.Expressions;
using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public class CardRepository : Repository<Card>, ICardRepository
{
  public CardRepository(MagicCollectionContext context) : base(context)
  {
  }

  public async Task<IEnumerable<Card>> GetAll(
    Func<IQueryable<Card>, IQueryable<Card>> transform = null,
    bool tracked = false
  )
  {
    var query = Includer(Context.Cards);
    query = transform == null ? DefaultTransform(query) : transform(query);
    if (!tracked) query = query.AsNoTracking();
    return await query.ToListAsync();
  }

  public async Task<Card> Get(Guid id) =>
    await Includer(Context.Set<Card>()).FirstOrDefaultAsync(e => e.Id == id);

  public async Task<Card> GetUntracked(Guid id) =>
    await Includer(Context.Set<Card>())
      .AsNoTracking()
      .FirstOrDefaultAsync(e => e.Id == id);

  public async Task<Card> Find(Expression<Func<Card, bool>> predicate) =>
    await Includer(Context.Set<Card>()).FirstOrDefaultAsync(predicate);

  private IQueryable<Card> Includer(IQueryable<Card> query) => query;

  private IQueryable<Card> DefaultTransform(IQueryable<Card> query) => query.OrderBy(e => e.Name);
  
  public async Task Commit() => await Context.SaveChangesAsync();
}
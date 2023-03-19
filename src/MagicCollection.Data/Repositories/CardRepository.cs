using System.Linq.Expressions;
using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public class CardRepository : ICardRepository
{
  
  private readonly MagicCollectionContext _context;
  
  public CardRepository(MagicCollectionContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Card>> GetAll(
    Func<IQueryable<Card>, IQueryable<Card>> transform = null,
    bool tracked = false
  )
  {
    var query = Includer(_context.Cards);
    query = transform == null ? DefaultTransform(query) : transform(query);
    if (!tracked) query = query.AsNoTracking();
    return await query.ToListAsync();
  }

  public async Task<Card> Get(Guid id) =>
    await Includer(_context.Set<Card>()).FirstOrDefaultAsync(e => e.Id == id);

  public async Task<Card> GetUntracked(Guid id) =>
    await Includer(_context.Set<Card>())
      .AsNoTracking()
      .FirstOrDefaultAsync(e => e.Id == id);

  public async Task<Card> Find(Expression<Func<Card, bool>> predicate) =>
    await Includer(_context.Set<Card>()).FirstOrDefaultAsync(predicate);

  private IQueryable<Card> Includer(IQueryable<Card> query) => query;

  private IQueryable<Card> DefaultTransform(IQueryable<Card> query) => query.OrderBy(e => e.Name);
  
  public async Task Commit() => await _context.SaveChangesAsync();
}
using System.Linq.Expressions;
using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public class CardRepository : EntityRepository<Card>, ICardRepository
{
  public CardRepository(MagicCollectionContext context) : base(context)
  {
  }
  
  protected override IQueryable<Card> DefaultTransform(IQueryable<Card> query) => query.OrderBy(e => e.Name);
}
using MagicCollection.Data.Entities;

namespace MagicCollection.Data.Repositories.Cards;

public class CardRepository : EntityRepository<Card>, ICardRepository
{
  public CardRepository(MagicCollectionContext context) : base(context)
  {
  }
  
  protected override IQueryable<Card> DefaultTransform(IQueryable<Card> query) => query.OrderBy(e => e.Name);
}
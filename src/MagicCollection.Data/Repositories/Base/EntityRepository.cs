using MagicCollection.Data.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public abstract class EntityRepository<T> : Repository<T>, IEntityRepository<T> where T : class, IEntity, new()
{
  public EntityRepository(MagicCollectionContext context) : base(context)
  {
  }

  public async Task<T> Get(Guid id) =>
    await Includer(Context.Set<T>()).FirstOrDefaultAsync(e => e.Id == id);

  public async Task<T> GetUntracked(Guid id) =>
    await Includer(Context.Set<T>())
      .AsNoTracking()
      .FirstOrDefaultAsync(e => e.Id == id);
}
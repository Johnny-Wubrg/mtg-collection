namespace MagicCollection.Data.Repositories;

public interface IRepository<T>
{
  Task SaveChanges();
}

public abstract class Repository<T> : IRepository<T>
{
  protected readonly MagicCollectionContext Context;

  protected Repository(MagicCollectionContext context)
  {
    Context = context;
  }

  public async Task SaveChanges()
  {
    await Context.SaveChangesAsync();
  }
}
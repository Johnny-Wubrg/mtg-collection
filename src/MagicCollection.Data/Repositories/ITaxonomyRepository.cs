using MagicCollection.Data.Entities.Interfaces;

namespace MagicCollection.Data.Repositories;

public interface ITaxonomyRepository<T> where T : class, ITaxonomy, new()
{
  Task<T> GetOrCreate(string id, CancellationToken cancellationToken = default);
}
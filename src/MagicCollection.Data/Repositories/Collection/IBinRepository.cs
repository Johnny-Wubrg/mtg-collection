using MagicCollection.Data.Entities;

namespace MagicCollection.Data.Repositories.Collection;

public interface IBinRepository : IEntityRepository<Bin>
{
  Task<Bin> GetOrCreate(string label, CancellationToken cancellationToken = default);
}
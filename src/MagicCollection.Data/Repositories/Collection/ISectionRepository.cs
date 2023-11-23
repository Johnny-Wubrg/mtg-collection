using MagicCollection.Data.Entities;

namespace MagicCollection.Data.Repositories.Collection;

public interface ISectionRepository : IEntityRepository<Section>
{
  Task<Section> GetOrCreate(string label, CancellationToken cancellationToken = default);
}
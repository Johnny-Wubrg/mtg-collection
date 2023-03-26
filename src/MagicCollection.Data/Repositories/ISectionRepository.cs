using MagicCollection.Data.Entities;

namespace MagicCollection.Data.Repositories;

public interface ISectionRepository : IRepository<Section>
{
  Task<Section> GetOrCreate(string label, CancellationToken cancellationToken = default);
}
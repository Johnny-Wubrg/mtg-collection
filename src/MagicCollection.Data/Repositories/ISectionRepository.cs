using MagicCollection.Data.Entities;

namespace MagicCollection.Data.Repositories;

public interface ISectionRepository
{
  Task<Section> GetOrCreate(string label, CancellationToken cancellationToken = default);
}
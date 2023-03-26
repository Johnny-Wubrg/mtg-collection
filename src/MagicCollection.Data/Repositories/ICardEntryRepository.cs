using MagicCollection.Data.Entities;

namespace MagicCollection.Data.Repositories;

public interface ICardEntryRepository
{
  Task<CardEntry> Get(Guid printId, string languageId, string treatmentId, Guid sectionId,
    CancellationToken cancellationToken = default);

  Task AddOrUpdate(Guid printId, string languageId, string treatmentId, Guid sectionId, int quantity,
    CancellationToken cancellationToken = default);
}
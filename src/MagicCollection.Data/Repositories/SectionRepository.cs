using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public class SectionRepository : EntityRepository<Section>, ISectionRepository
{
  public SectionRepository(MagicCollectionContext context) : base(context)
  {
  }

  public async Task<Section> GetOrCreate(string label, CancellationToken cancellationToken = default)
  {
    var found = await Context.Sections.FirstOrDefaultAsync(e => e.Label == label, cancellationToken: cancellationToken);
    if (found is not null) return found;

    var newRecord = new Section { Label = label };
    await Context.Sections.AddAsync(newRecord, cancellationToken);

    return newRecord;
  }
}
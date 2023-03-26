using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public class SectionRepository : ISectionRepository
{
  
  private readonly MagicCollectionContext _context;
  
  public SectionRepository(MagicCollectionContext context)
  {
    _context = context;
  }

  public async Task<Section> GetOrCreate(string label, CancellationToken cancellationToken = default)
  {
    var found = await _context.Sections.FirstOrDefaultAsync(e => e.Label == label, cancellationToken: cancellationToken);
    if (found is not null) return found;

    var newRecord = new Section { Label = label };
    await _context.Sections.AddAsync(newRecord, cancellationToken);

    return newRecord;
  }
}
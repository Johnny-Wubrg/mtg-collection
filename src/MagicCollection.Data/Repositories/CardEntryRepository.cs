using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public class CardEntryRepository : EntityRepository<CardEntry>, ICardEntryRepository
{
  public CardEntryRepository(MagicCollectionContext context) : base(context)
  {
  }

  public async Task<CardEntry> Get(Guid printId, string languageId, string treatmentId,
    Guid? sectionId, CancellationToken cancellationToken = default)
  {
    return await Context.CardEntries.FirstOrDefaultAsync(ce =>
      ce.PrintId == printId &&
      ce.LanguageIdentifier == languageId &&
      ce.TreatmentIdentifier == treatmentId &&
      ce.SectionId == sectionId, cancellationToken: cancellationToken);
  }

  public async Task AddOrUpdate(Guid printId, string languageId, string treatmentId, Guid? sectionId, int quantity,
    CancellationToken cancellationToken = default)
  {
    var found = await Get(printId, languageId, treatmentId, sectionId, cancellationToken);

    if (found is not null)
    {
      found.Quantity += quantity;
      return;
    }

    var entry = new CardEntry
    {
      PrintId = printId,
      LanguageIdentifier = languageId,
      TreatmentIdentifier = treatmentId,
      Quantity = quantity,
      SectionId = sectionId
    };

    await Context.CardEntries.AddAsync(entry, cancellationToken);
  }
}
using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data.Repositories;

public class CardEntryRepository : ICardEntryRepository
{
  private readonly MagicCollectionContext _context;
  private readonly ITaxonomyRepository<Treatment> _treatmentRepository;
  private readonly ITaxonomyRepository<Language> _languageRepository;

  public CardEntryRepository(MagicCollectionContext context,
    ITaxonomyRepository<Treatment> treatmentRepository,
    ITaxonomyRepository<Language> languageRepository)
  {
    _context = context;
    _treatmentRepository = treatmentRepository;
    _languageRepository = languageRepository;
  }

  public async Task<CardEntry> Get(Guid printId, string languageId, string treatmentId,
    Guid sectionId, CancellationToken cancellationToken = default)
  {
    return await _context.CardEntries.FirstOrDefaultAsync(ce =>
      ce.PrintId == printId &&
      ce.LanguageIdentifier == languageId &&
      ce.TreatmentIdentifier == treatmentId &&
      ce.SectionId == sectionId, cancellationToken: cancellationToken);
  }

  public async Task AddOrUpdate(Guid printId, string languageId, string treatmentId, Guid sectionId, int quantity,
    CancellationToken cancellationToken = default)
  {
    var found = await Get(printId, languageId, treatmentId, sectionId, cancellationToken);

    if (found is not null)
    {
      found.Quantity += quantity;
      return;
    }


    var lang = await _languageRepository.GetOrCreate(languageId, cancellationToken);
    var treatment = await _treatmentRepository.GetOrCreate(treatmentId, cancellationToken);
    
    var entry = new CardEntry
    {
      PrintId = printId,
      Language = lang,
      Treatment = treatment,
      Quantity = quantity,
      SectionId = sectionId
    };

    await _context.CardEntries.AddAsync(entry, cancellationToken);
  }
}
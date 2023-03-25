using MagicCollection.Data;
using MagicCollection.Data.Entities;
using MagicCollection.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Services.BulkData;

/// <inheritdoc />
public class ImportCollectionService : IImportCollectionService
{
  private readonly DbContextOptions<MagicCollectionContext> _contextOptions;
  private readonly ITaxonomyRepository<Treatment> _treatmentRepository;
  private readonly ITaxonomyRepository<Language> _languageRepository;
  private readonly ICardEntryRepository _cardEntryRepository;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="contextOptions"></param>
  /// <param name="treatmentRepository"></param>
  /// <param name="languageRepository"></param>
  /// <param name="cardEntryRepository"></param>
  public ImportCollectionService(
    DbContextOptions<MagicCollectionContext> contextOptions,
    ITaxonomyRepository<Treatment> treatmentRepository,
    ITaxonomyRepository<Language> languageRepository,
    ICardEntryRepository cardEntryRepository
  )
  {
    _contextOptions = contextOptions;
    _treatmentRepository = treatmentRepository;
    _languageRepository = languageRepository;
    _cardEntryRepository = cardEntryRepository;
  }

  /// <inheritdoc />
  public async Task UploadCollection(IEnumerable<Dictionary<string, string>> entries,
    CancellationToken cancellationToken)
  {
    foreach (var entry in entries) await CreateEntry(entry, cancellationToken);
  }

  private async Task CreateEntry(Dictionary<string, string> row, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    await using var context = new MagicCollectionContext(_contextOptions);

    var print = await context.Prints.FirstAsync(p =>
      p.Edition.Code.ToLower() == row["Set"].ToLower() &&
      p.CollectorNumber.ToLower() == row["Collector Number"].ToLower(), cancellationToken);

    var section = await GetSection(context, row["Location"], cancellationToken);

    var langId = string.IsNullOrWhiteSpace(row["Language"]) ? "en" : row["Language"].ToLower();
    var foilId = string.IsNullOrWhiteSpace(row["Foil"]) ? "nonfoil" : row["Foil"].ToLower();

    await _cardEntryRepository.AddOrUpdate(context, print.Id, langId, foilId, section.Id, int.Parse(row["Quantity"]),
      cancellationToken);
    await context.SaveChangesAsync(cancellationToken);
  }


  private async Task<Section> GetSection(MagicCollectionContext context, string label,
    CancellationToken cancellationToken = default)
  {
    var found = await context.Sections.FirstOrDefaultAsync(e => e.Label == label, cancellationToken: cancellationToken);
    if (found is not null) return found;

    var newRecord = new Section { Label = label };
    await context.Sections.AddAsync(newRecord, cancellationToken);

    return newRecord;
  }
}
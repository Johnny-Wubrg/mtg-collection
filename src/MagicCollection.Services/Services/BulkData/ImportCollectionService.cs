using MagicCollection.Data;
using MagicCollection.Data.Entities;
using MagicCollection.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Services.BulkData;

/// <inheritdoc />
public class ImportCollectionService : IImportCollectionService
{
  private readonly DbContextOptions<MagicCollectionContext> _contextOptions;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="contextOptions"></param>
  public ImportCollectionService(
    DbContextOptions<MagicCollectionContext> contextOptions
  )
  {
    _contextOptions = contextOptions;
  }

  /// <inheritdoc />
  public async Task UploadCollection(IEnumerable<Dictionary<string, string>> entries,
    CancellationToken cancellationToken)
  {
    foreach (var entry in entries) await CreateEntry(entry, cancellationToken);
  }

  private async Task CreateEntry(IReadOnlyDictionary<string, string> row, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    await using var context = new MagicCollectionContext(_contextOptions);
    var cardEntryRepository = new CardEntryRepository(
      context,
      new TaxonomyRepository<Treatment>(context),
      new TaxonomyRepository<Language>(context)
    );

    var print = await context.Prints.FirstAsync(p =>
      p.Edition.Code.ToLower() == row["Set"].ToLower() &&
      p.CollectorNumber.ToLower() == row["Collector Number"].ToLower(), cancellationToken);

    var sectionRepository = new SectionRepository(context);

    var section = await sectionRepository.GetOrCreate(row["Location"], cancellationToken);

    var langId = string.IsNullOrWhiteSpace(row["Language"]) ? "en" : row["Language"].ToLower();
    var foilId = string.IsNullOrWhiteSpace(row["Foil"]) ? "nonfoil" : row["Foil"].ToLower();

    await cardEntryRepository.AddOrUpdate(print.Id, langId, foilId, section.Id, int.Parse(row["Quantity"]),
      cancellationToken);
    await context.SaveChangesAsync(cancellationToken);
  }
}
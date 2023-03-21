using MagicCollection.Data;
using MagicCollection.Data.Entities;
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
  public ImportCollectionService(DbContextOptions<MagicCollectionContext> contextOptions)
  {
    _contextOptions = contextOptions;
  }

  /// <inheritdoc />
  public async Task UploadCollection(IEnumerable<Dictionary<string, string>> entries, CancellationToken cancellationToken)
  {
    foreach (var entry in entries) await CreateEntry(entry, cancellationToken);
  }

  private async Task CreateEntry(Dictionary<string, string> row, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    await using var context = new MagicCollectionContext(_contextOptions);

    var print = await context.Prints.FirstAsync(p =>
      p.Edition.Code.ToLower() == row["Set"].ToLower() &&
      p.CollectorNumber.ToLower() == row["Collector Number"].ToLower()
    );

    var section = await GetSection(context, row["Location"]);

    var entry = new CardEntry
    {
      Print = print,
      Language = await GetLanguage(context, row["Language"].ToLower()),
      Treatment = await GetTreatment(context, row["Foil"]),
      Quantity = int.Parse(row["Quantity"]),
      Section = section
    };

    await context.CardEntries.AddAsync(entry);
    await context.SaveChangesAsync();
  }

  private async Task<Language> GetLanguage(MagicCollectionContext context, string id)
  {
    var identifier = string.IsNullOrEmpty(id) ? "en" : id;
    var found = await context.Languages.FirstOrDefaultAsync(l => l.Identifier == identifier);
    if (found is not null) return found;

    var newLang = new Language
    {
      Identifier = id,
      Label = id
    };

    await context.Languages.AddAsync(newLang);

    return newLang;
  }

  private async Task<Treatment> GetTreatment(MagicCollectionContext context, string id)
  {
    var identifier = string.IsNullOrEmpty(id) ? "nonfoil" : id;
    var found = await context.Treatments.FirstOrDefaultAsync(l => l.Identifier == identifier);
    if (found is not null) return found;

    var newTreatment = new Treatment
    {
      Identifier = id,
      Label = id
    };

    await context.Treatments.AddAsync(newTreatment);

    return newTreatment;
  }


  private async Task<Section> GetSection(MagicCollectionContext context, string label)
  {
    var found = await context.Sections.FirstOrDefaultAsync(e => e.Label == label);
    if (found is not null) return found;

    var newRecord = new Section { Label = label };
    await context.Sections.AddAsync(newRecord);

    return newRecord;
  }
}
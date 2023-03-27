using System.Text.Json;
using MagicCollection.Services.BulkData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using ScryNet.Models;

namespace MagicTutors.Api.Controllers;

/// <inheritdoc />
[ApiController]
[Route("[controller]")]
public class UtilitiesController : ControllerBase
{
  private readonly ILogger<UtilitiesController> _logger;
  private readonly IImportCardsService _importCardsService;
  private readonly IImportCollectionService _importCollectionService;

  /// <inheritdoc />
  public UtilitiesController(
    ILogger<UtilitiesController> logger,
    IImportCardsService importCardsService,
    IImportCollectionService importCollectionService
  )
  {
    _logger = logger;
    _importCardsService = importCardsService;
    _importCollectionService = importCollectionService;
  }

  /// <summary>
  /// Upload cards from a Scryfall JSON export.
  /// </summary>
  /// <param name="file">Scryfall Export File</param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  [HttpPost("import-cards", Name = "UploadCards")]
  public async Task<ActionResult> UploadCards(IFormFile file, CancellationToken cancellationToken)
  {
    var options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true,
    };

    var cards = await JsonSerializer.DeserializeAsync<ScryfallCard[]>(file.OpenReadStream(), options,
      cancellationToken);
    cards = cards
      .Where(e => e.Games.Contains("paper"))
      .ToArray();

    await _importCardsService.UploadCards(cards, cancellationToken);

    return new EmptyResult();
  }

  /// <summary>
  /// Upload collection from a formatted CSV.
  /// </summary>
  /// <param name="file">Collection CSV File</param>
  /// <param name="cancellationToken"></param>
  [HttpPost("import-collection", Name = "UploadCollection")]
  public async Task UploadCollection(IFormFile file, CancellationToken cancellationToken)
  {
    var parser = new TextFieldParser(file.OpenReadStream());
    parser.TextFieldType = FieldType.Delimited;
    parser.SetDelimiters(",");

    var headers = parser.ReadFields();
    var records = new List<Dictionary<string, string>>();

    while (!parser.EndOfData)
    {
      cancellationToken.ThrowIfCancellationRequested();

      var row = parser.ReadFields();
      var entry = new Dictionary<string, string>();

      if (row == null) return;

      var existing = records.FirstOrDefault(e =>
        e["Set"] == row[2] &&
        e["Collector Number"] == row[3] &&
        e["Foil"] == row[4] &&
        e["Language"] == row[5] &&
        e["Location"] == row[6]
      );

      if (existing is not null)
      {
        var currentQty = int.Parse(existing["Quantity"]);
        var adtlQty = int.Parse(row[0]);
        existing["Quantity"] = (currentQty + adtlQty).ToString();

        continue;
      }

      for (var i = 0; i < row.Length; i++)
      {
        if (headers != null) entry[headers[i]] = row[i];
      }

      records.Add(entry);
    }

    await _importCollectionService.UploadCollection(records, cancellationToken);
  }
}
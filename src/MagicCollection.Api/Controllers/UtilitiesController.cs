using System.Text;
using System.Text.Json;
using MagicCollection.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScryNet.Models;

namespace MagicTutors.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UtilitiesController : ControllerBase
{
  private readonly ILogger<UtilitiesController> _logger;
  private readonly IBulkDataService _bulkDataService;

  public UtilitiesController(ILogger<UtilitiesController> logger, IBulkDataService bulkDataService)
  {
    _logger = logger;
    _bulkDataService = bulkDataService;
  }

  /// <summary>
  /// Upload cards from a Scryfall JSON export.
  /// </summary>
  /// <param name="file">Scryfall Export File</param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  [HttpPost("upload", Name = "UploadCards")]
  public async Task<ActionResult> UploadCards(IFormFile file, CancellationToken cancellationToken)
  {
    var result = new StringBuilder();
    var cards = new ScryfallCard[] { };

    var options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true,
    };

    cards = await JsonSerializer.DeserializeAsync<ScryfallCard[]>(file.OpenReadStream(), options);
    cards = cards.Where(e => e.Legalities.Vintage != "not_legal").ToArray();

    await _bulkDataService.UploadCards(cards, cancellationToken);

    return new EmptyResult();
  }
}
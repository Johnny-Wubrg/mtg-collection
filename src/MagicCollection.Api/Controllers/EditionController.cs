using MagicCollection.Services;
using MagicCollection.Services.Cards;
using MagicCollection.Services.Models.Cards;
using Microsoft.AspNetCore.Mvc;

namespace MagicCollection.Api.Controllers;

/// <summary>
/// Controller for editions.
/// </summary>
[ApiController]
[Route("[controller]")]
public class EditionController : ControllerBase
{
  private readonly IPrintService _printService;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="printService"></param>
  public EditionController(IPrintService printService)
  {
    _printService = printService;
  }
  
  /// <summary>
  /// Get specific printing of a card by set code and collector number.
  /// </summary>
  /// <returns></returns>
  [HttpGet("{setCode}/prints/{cn}")]
  public async Task<IEnumerable<PrintModel>> GetPrints(string setCode, string cn) => await _printService.GetBySetCode(setCode, cn);

}
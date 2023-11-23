using MagicCollection.Services;
using MagicCollection.Services.Cards;
using MagicCollection.Services.Models.Cards;
using MagicCollection.Services.Models.Request;

namespace MagicCollection.Api.Controllers;

/// <summary>
/// Controller for cards.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CardsController : ControllerBase
{
  private readonly ICardService _cardService;
  private readonly IPrintService _printService;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="cardService"></param>
  /// <param name="printService"></param>
  public CardsController(ICardService cardService, IPrintService printService)
  {
    _cardService = cardService;
    _printService = printService;
  }

  /// <summary>
  /// Search for a card by name
  /// </summary>
  /// <returns></returns>
  /// <param name="name" example="storm">Search query</param>
  [HttpGet("search")]
  public async Task<IEnumerable<CardModel>> Search(string name) => await _cardService.Search(name);


  /// <summary>
  /// Search for a card by name
  /// </summary>
  /// <returns></returns>
  /// <param name="model">Search query</param>
  [HttpGet("search/prints")]
  public async Task<IEnumerable<PrintModel>> SearchPrints([FromQuery] PrintSearchModel model) => await _printService.Search(model);

  /// <summary>
  /// Get a card by its Scryfall oracle ID
  /// </summary>
  /// <returns></returns>
  /// <param name="id" example="000d5588-5a4c-434e-988d-396632ade42c">Oracle ID of the card</param>
  [HttpGet("{id}")]
  public async Task<CardModel> Get(Guid id) => await _cardService.Get(id);

  /// <summary>
  /// Get all printings of a card by its Scryfall oracle ID
  /// </summary>
  /// <returns></returns>
  /// <param name="id" example="000d5588-5a4c-434e-988d-396632ade42c">Oracle ID of the card</param>
  [HttpGet("{id}/prints")]
  public async Task<IEnumerable<PrintModel>> GetPrints(Guid id) => await _printService.GetByCardId(id);
}
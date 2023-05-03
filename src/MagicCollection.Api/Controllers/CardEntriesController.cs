using MagicCollection.Services;
using MagicCollection.Services.Models.Collection;
using MagicCollection.Services.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace MagicCollection.Api.Controllers;

/// <summary>
/// Controller for card entries
/// </summary>
[ApiController]
[Route("card-entries")]
public class CardEntriesController : ControllerBase
{
  private readonly ICardEntryService _cardEntryService;

  /// <summary>
  /// Instantiate a controller for routing card entry requests
  /// </summary>
  /// <param name="cardEntryService"></param>
  public CardEntriesController(ICardEntryService cardEntryService)
  {
    _cardEntryService = cardEntryService;
  }

  /// <summary>
  /// Adds a quantity of cards to an entry to a collection
  /// </summary>
  /// <param name="model"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  [HttpPost]
  public async Task Add(CardEntryRequestModel model, CancellationToken cancellationToken)
  {
    await _cardEntryService.Add(model, cancellationToken);
  }

  /// <summary>
  /// Bulk add cards to a collection
  /// </summary>
  /// <param name="models"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  [HttpPost("bulk")]
  public async Task AddMany(List<CardEntryRequestModel> models, CancellationToken cancellationToken)
  {
    await _cardEntryService.AddRange(models, cancellationToken);
  }
}
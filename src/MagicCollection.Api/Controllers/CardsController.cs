using MagicCollection.Services;
using MagicCollection.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicCollection.Api.Controllers;

/// <summary>
/// Controller for cards.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CardsController : ControllerBase
{
  private readonly ICardService _service;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="service"></param>
  public CardsController(ICardService service)
  {
    _service = service;
  }

  /// <summary>
  /// Search for a card be name
  /// </summary>
  /// <returns></returns>
  /// <param name="name" example="storm">Search query</param>
  [HttpGet("search")]
  public async Task<IEnumerable<CardModel>> Search(string name) => await _service.Search(name);

  /// <summary>
  /// Get a card by its Scryfall oracle ID
  /// </summary>
  /// <returns></returns>
  /// <param name="id" example="000d5588-5a4c-434e-988d-396632ade42c">Oracle ID of the card</param>
  [HttpGet("{id}")]
  public async Task<CardModel> Get(Guid id) => await _service.Get(id);
}
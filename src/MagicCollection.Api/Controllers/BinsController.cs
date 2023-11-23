using MagicCollection.Services.Collection;
using MagicCollection.Services.Models.Collection;

namespace MagicCollection.Api.Controllers;

/// <summary>
/// Controller for bins
/// </summary>
[ApiController]
[Route("[controller]")]
[Tags("Collections")]
public class BinsController
{
  private readonly IBinService _service;

  /// <summary>
  /// Instantiate a controller for routing bin requests
  /// </summary>
  public BinsController(IBinService service)
  {
    _service = service;
  }

  /// <summary>
  /// Get all bins
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  public Task<IEnumerable<BinModel>> GetAll() => _service.GetAll();

  /// <summary>
  /// Get a bin by its ID
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id:guid}")]
  public Task<BinModel> Get(Guid id) => _service.Get(id);
}
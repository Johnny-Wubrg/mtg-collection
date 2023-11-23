using MagicCollection.Services.Collection;
using MagicCollection.Services.Models.Collection;

namespace MagicCollection.Api.Controllers;

/// <summary>
/// Controller for sections
/// </summary>
[ApiController]
[Route("bins/{binId:guid}/[controller]")]
[Tags("Collections")]
public class SectionsController
{
  private readonly ISectionService _service;

  /// <summary>
  /// Instantiate a controller for routing section requests
  /// </summary>
  public SectionsController(ISectionService service)
  {
    _service = service;
  }

  /// <summary>
  /// Get all sections in a bin
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  public Task<IEnumerable<SectionModel>> GetAll(Guid binId) => _service.GetAllByBin(binId);

  /// <summary>
  /// Get a section in a bin by its ID
  /// </summary>
  /// <param name="id"></param>
  /// <param name="binId"></param>
  /// <returns></returns>
  [HttpGet("{id:guid}")]
  public Task<SectionModel> Get(Guid binId, Guid id) => _service.GetByBin(binId, id);
}
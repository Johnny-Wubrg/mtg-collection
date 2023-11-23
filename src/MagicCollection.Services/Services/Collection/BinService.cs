using AutoMapper;
using MagicCollection.Data.Repositories.Collection;
using MagicCollection.Services.Models.Collection;

namespace MagicCollection.Services.Collection;

/// <inheritdoc />
public class BinService : IBinService
{
  private readonly IMapper _mapper;
  private readonly IBinRepository _repo;

  /// <summary>
  ///     Creates an instance of the service.
  /// </summary>
  /// <param name="mapper">The Automapper instance.</param>
  /// <param name="repo">Data access repository.</param>
  public BinService(IMapper mapper, IBinRepository repo)
  {
    _mapper = mapper;
    _repo = repo;
  }

  /// <inheritdoc />
  public async Task<IEnumerable<BinModel>> GetAll() =>
    _mapper.Map<IEnumerable<BinModel>>(await _repo.GetAll());

  /// <inheritdoc />
  public async Task<BinModel> Get(Guid id) =>
    _mapper.Map<BinModel>(await _repo.Get(id));
}
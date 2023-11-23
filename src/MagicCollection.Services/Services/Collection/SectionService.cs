using AutoMapper;
using MagicCollection.Data.Repositories.Collection;
using MagicCollection.Services.Models.Collection;

namespace MagicCollection.Services.Collection;

/// <inheritdoc />
public class SectionService : ISectionService
{
  private readonly IMapper _mapper;
  private readonly ISectionRepository _repo;

  /// <summary>
  ///     Creates an instance of the service.
  /// </summary>
  /// <param name="mapper">The Automapper instance.</param>
  /// <param name="repo">Data access repository.</param>
  public SectionService(IMapper mapper, ISectionRepository repo)
  {
    _mapper = mapper;
    _repo = repo;
  }

  /// <inheritdoc />
  public async Task<IEnumerable<SectionModel>> GetAllByBin(Guid binId) =>
    _mapper.Map<IEnumerable<SectionModel>>(await _repo.GetAll(q => q.Where(e => e.Bin.Id == binId)));

  /// <inheritdoc />
  public async Task<SectionModel> GetByBin(Guid binId, Guid id) =>
    _mapper.Map<SectionModel>(await _repo.Find(e => e.Bin.Id == binId && e.Id == id));
}
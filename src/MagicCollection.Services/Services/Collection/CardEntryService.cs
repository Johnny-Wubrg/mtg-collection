using AutoMapper;
using MagicCollection.Data.Entities;
using MagicCollection.Data.Repositories;
using MagicCollection.Data.Repositories.Collection;
using MagicCollection.Services.Models.Collection;
using MagicCollection.Services.Models.Request;
using MagicCollection.Services.Models.Response;

namespace MagicCollection.Services.Collection;

/// <inheritdoc />
public class CardEntryService : ICardEntryService
{
  private readonly IMapper _mapper;

  private readonly ICardEntryRepository _repo;

  /// <summary>
  ///     Creates an instance of the service.
  /// </summary>
  /// <param name="mapper">The Automapper instance.</param>
  /// <param name="repo">Data access repository.</param>
  public CardEntryService(IMapper mapper, ICardEntryRepository repo)
  {
    _mapper = mapper;
    _repo = repo;
  }

  /// <inheritdoc />
  public async Task<PagedResponseModel<CardEntryModel>> GetPaged(int page = 1, int pageSize = 50)
  {
    var skip = (page - 1) * pageSize;
    var items = await _repo.GetAll(e => e.Skip(skip).Take(pageSize));
    var count = await _repo.Count();

    return new PagedResponseModel<CardEntryModel>
    {
      Data = _mapper.Map<IEnumerable<CardEntryModel>>(items),
      Page = page,
      TotalElements = count
    };
  }

  /// <inheritdoc />
  public async Task Add(CardEntryRequestModel model, CancellationToken cancellationToken = default)
  {
    await _repo.AddOrUpdate(model.Print, model.Language, model.Treatment, model.Section,
      model.Quantity, cancellationToken);
    await _repo.SaveChanges();
  }

  /// <inheritdoc />
  public async Task AddRange(IEnumerable<CardEntryRequestModel> models, CancellationToken cancellationToken = default)
  {
    foreach (var model in models)
    {
      await _repo.AddOrUpdate(model.Print, model.Language, model.Treatment, model.Section,
        model.Quantity, cancellationToken);
    }

    await _repo.SaveChanges();
  }
}
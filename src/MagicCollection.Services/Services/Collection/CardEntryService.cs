using AutoMapper;
using MagicCollection.Data.Entities;
using MagicCollection.Data.Repositories;
using MagicCollection.Data.Repositories.Collection;
using MagicCollection.Services.Models.Collection;
using MagicCollection.Services.Models.Request;

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
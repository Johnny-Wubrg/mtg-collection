using AutoMapper;
using MagicCollection.Data.Entities;
using MagicCollection.Data.Repositories;
using MagicCollection.Services.Models.Collection;

namespace MagicCollection.Services;

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
  public async Task Add(CardEntryModel model, CancellationToken cancellationToken = default)
  {
    await _repo.AddOrUpdate(model.Print.Id, model.Language.Identifier, model.Treatment.Identifier, model.Section.Id,
      model.Quantity, cancellationToken);
    await _repo.SaveChanges();
  }
}
using AutoMapper;
using MagicCollection.Data.Repositories;
using MagicCollection.Data.Repositories.Cards;
using MagicCollection.Services.Models.Cards;

namespace MagicCollection.Services.Cards;

/// <inheritdoc />
public class CardService : ICardService
{
  /// <summary>
  ///     The Automapper instance.
  /// </summary>
  private readonly IMapper _mapper;

  /// <summary>
  ///     Data access repository.
  /// </summary>
  private readonly ICardRepository _repo;

  /// <summary>
  ///     Creates an instance of the service.
  /// </summary>
  /// <param name="mapper">The Automapper instance.</param>
  /// <param name="repo">Data access repository.</param>
  public CardService(IMapper mapper, ICardRepository repo)
  {
    _mapper = mapper;
    _repo = repo;
  }

  /// <inheritdoc />
  public async Task<IEnumerable<CardModel>> GetAll() =>
    _mapper.Map<IEnumerable<CardModel>>(await _repo.GetAll());

  /// <inheritdoc />
  public async Task<IEnumerable<CardModel>> Search(string name)
  {
    if (string.IsNullOrEmpty(name)) return Array.Empty<CardModel>();
    
    return _mapper.Map<IEnumerable<CardModel>>(await _repo.GetAll(q =>
      q.Where(c => c.Name.ToLower().Contains(name.ToLower())).Take(10)));
  }

  /// <inheritdoc />
  public async Task<CardModel> Get(Guid id) =>
    _mapper.Map<CardModel>(await _repo.Get(id));
}
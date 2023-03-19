using AutoMapper;
using MagicCollection.Data.Repositories;
using MagicCollection.Services.Models;

namespace MagicCollection.Services;

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
  private readonly ICardRepository _cardRepo;

  /// <summary>
  ///     Creates an instance of the service.
  /// </summary>
  /// <param name="mapper">The Automapper instance.</param>
  /// <param name="cardRepo">Data access repository.</param>
  public CardService(IMapper mapper, ICardRepository cardRepo)
  {
    _mapper = mapper;
    _cardRepo = cardRepo;
  }

  /// <inheritdoc />
  public async Task<IEnumerable<CardModel>> GetAll() =>
    _mapper.Map<IEnumerable<CardModel>>(await _cardRepo.GetAll());

  /// <inheritdoc />
  public async Task<IEnumerable<CardModel>> Search(string name)
  {
    return _mapper.Map<IEnumerable<CardModel>>(await _cardRepo.GetAll(q =>
      q.Where(c => c.Name.ToLower().Contains(name.ToLower())).Take(10)));
  }

  /// <inheritdoc />
  public async Task<CardModel> Get(Guid id) =>
    _mapper.Map<CardModel>(await _cardRepo.Get(id));
}
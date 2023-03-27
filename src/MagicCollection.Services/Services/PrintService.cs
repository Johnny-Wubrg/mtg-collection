using AutoMapper;
using MagicCollection.Data.Repositories;
using MagicCollection.Services.Models.Cards;

namespace MagicCollection.Services;

/// <inheritdoc />
public class PrintService : IPrintService
{
  /// <summary>
  ///     The Automapper instance.
  /// </summary>
  private readonly IMapper _mapper;

  /// <summary>
  ///     Data access repository.
  /// </summary>
  private readonly IPrintRepository _repo;

  /// <summary>
  ///     Creates an instance of the service.
  /// </summary>
  /// <param name="mapper">The Automapper instance.</param>
  /// <param name="repo">Data access repository.</param>
  public PrintService(IMapper mapper, IPrintRepository repo)
  {
    _mapper = mapper;
    _repo = repo;
  }

  /// <inheritdoc />
  public async Task<IEnumerable<PrintModel>> GetByCardId(Guid cardId)
  {
    var prints =
      await _repo.GetAll(q => q.Where(p => p.CardId == cardId).OrderByDescending(p => p.Edition.DateReleased));
    return _mapper.Map<IEnumerable<PrintModel>>(prints);
  }
}
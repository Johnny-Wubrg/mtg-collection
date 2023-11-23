using AutoMapper;
using MagicCollection.Data.Repositories;
using MagicCollection.Data.Repositories.Cards;
using MagicCollection.Services.Models.Cards;
using MagicCollection.Services.Models.Request;

namespace MagicCollection.Services.Cards;

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
  public async Task<IEnumerable<PrintModel>> Search(PrintSearchModel model)
  {
    return _mapper.Map<IEnumerable<PrintModel>>(await _repo.GetAll(q =>
      q.Where(c =>
          (string.IsNullOrEmpty(model.Name) || c.Card.Name.ToLower().Contains(model.Name.ToLower())) &&
          (string.IsNullOrEmpty(model.Set) || c.Edition.Code.ToLower() == model.Set.ToLower()) &&
          (string.IsNullOrEmpty(model.CollectorNumber) ||
           c.CollectorNumber.ToLower() == model.CollectorNumber.ToLower())
        )
        .OrderBy(p => p.Card.Name)
        .ThenByDescending(p => p.Edition.DateReleased)
        .ThenBy(p => p.CollectorNumber)
        .Take(100)
    ));
  }

  /// <inheritdoc />
  public async Task<IEnumerable<PrintModel>> GetByCardId(Guid cardId)
  {
    var prints =
      await _repo.GetAll(q => q.Where(p => p.CardId == cardId).OrderByDescending(p => p.Edition.DateReleased));
    return _mapper.Map<IEnumerable<PrintModel>>(prints);
  }

  /// <inheritdoc />
  public async Task<IEnumerable<PrintModel>> GetBySetCode(string setCode, string cn)
  {
    var prints =
      await _repo.GetAll(q => q
        .Where(p => p.Edition.Code.ToLower() == setCode.ToLower() && p.CollectorNumber == cn)
        .OrderByDescending(p => p.Edition.DateReleased));
    return _mapper.Map<IEnumerable<PrintModel>>(prints);
  }
}
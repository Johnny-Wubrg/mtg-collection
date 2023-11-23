using AutoMapper;
using MagicCollection.Data.Entities.Interfaces;
using MagicCollection.Data.Repositories;
using MagicCollection.Services.Models.Interfaces;

namespace MagicCollection.Services.Cards;

/// <inheritdoc />
public class TaxonomyService<TEntity, TModel> : ITaxonomyService<TModel> where TEntity : class, ITaxonomy, new()
  where TModel : class, ITaxonomyModel, new()
{
  /// <summary>
  ///     The Automapper instance.
  /// </summary>
  private readonly IMapper _mapper;

  /// <summary>
  ///     Data access repository.
  /// </summary>
  private readonly ITaxonomyRepository<TEntity> _repo;

  /// <summary>
  ///     Creates an instance of the service.
  /// </summary>
  /// <param name="mapper">The Automapper instance.</param>
  /// <param name="repo">Data access repository.</param>
  public TaxonomyService(IMapper mapper, ITaxonomyRepository<TEntity> repo)
  {
    _mapper = mapper;
    _repo = repo;
  }

  /// <inheritdoc />
  public async Task<IEnumerable<TModel>> GetAll() => _mapper.Map<IEnumerable<TModel>>(await _repo.GetAll());
}
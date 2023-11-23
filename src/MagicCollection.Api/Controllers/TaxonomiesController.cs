using MagicCollection.Services;
using MagicCollection.Services.Cards;
using MagicCollection.Services.Models.Cards;
using Microsoft.AspNetCore.Mvc;

namespace MagicCollection.Api.Controllers;

/// <inheritdoc />
[ApiController]
[Route("[controller]")]
public class TaxonomiesController : ControllerBase
{
  private readonly ITaxonomyService<TreatmentModel> _treatmentService;
  private readonly ITaxonomyService<RarityModel> _rarityService;
  private readonly ITaxonomyService<LanguageModel> _langService;
  private readonly ITaxonomyService<EditionTypeModel> _editionTypeService;

  /// <inheritdoc />
  public TaxonomiesController(
    ITaxonomyService<TreatmentModel> treatmentService,
    ITaxonomyService<RarityModel> rarityService,
    ITaxonomyService<LanguageModel> langService,
    ITaxonomyService<EditionTypeModel> editionTypeService)
  {
    _treatmentService = treatmentService;
    _rarityService = rarityService;
    _langService = langService;
    _editionTypeService = editionTypeService;
  }

  /// <summary>
  /// Get all available treatments
  /// </summary>
  /// <returns></returns>
  [HttpGet("treatments")]
  public async Task<IEnumerable<TreatmentModel>> GetTreatments() => await _treatmentService.GetAll();

  /// <summary>
  /// Get all available rarities
  /// </summary>
  /// <returns></returns>
  [HttpGet("rarities")]
  public async Task<IEnumerable<RarityModel>> GetRarities() => await _rarityService.GetAll();

  /// <summary>
  /// Get all available languages
  /// </summary>
  /// <returns></returns>
  [HttpGet("languages")]
  public async Task<IEnumerable<LanguageModel>> GetLanguages() => await _langService.GetAll();

  /// <summary>
  /// Get all available edition types
  /// </summary>
  /// <returns></returns>
  [HttpGet("edition-types")]
  public async Task<IEnumerable<EditionTypeModel>> GetEditionTypes() => await _editionTypeService.GetAll();
}
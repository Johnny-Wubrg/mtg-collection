using AutoMapper;
using MagicCollection.Data;
using MagicCollection.Data.Entities;
using MagicCollection.Data.Repositories;
using MagicCollection.Data.Repositories.Cards;
using MagicCollection.Data.Repositories.Collection;
using MagicCollection.Services.BulkData;
using MagicCollection.Services.Cards;
using MagicCollection.Services.Collection;
using MagicCollection.Services.Models.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MagicCollection.Services.Extensions;

/// <summary>
/// Extensions for MagicCollection
/// </summary>
public static class MagicCollectionExtensions
{

  /// <summary>
  /// Instantiate MagicCollection services.
  /// </summary>
  /// <param name="services"></param>
  /// <param name="config"></param>
  public static void AddMagicCollection(this IServiceCollection services, IConfiguration config)
  {
    AddRepositories(services, config);
    AddServices(services, config);
    AddDatabase(services, config);
    AddAutoMapper(services);
  }

  private static void AddRepositories(IServiceCollection services, IConfiguration config)
  {
    services.AddTransient(typeof(ITaxonomyRepository<>), typeof(TaxonomyRepository<>));
    services.AddTransient<ICardRepository, CardRepository>();
    services.AddTransient<ICardEntryRepository, CardEntryRepository>();
    services.AddTransient<ISectionRepository, SectionRepository>();
    services.AddTransient<IPrintRepository, PrintRepository>();
    services.AddTransient<IBinRepository, BinRepository>();
    services.AddTransient<ISectionRepository, SectionRepository>();
  }

  private static void AddServices(IServiceCollection services, IConfiguration config)
  {
    services.AddTransient<ITaxonomyService<TreatmentModel>, TaxonomyService<Treatment, TreatmentModel>>();
    services.AddTransient<ITaxonomyService<RarityModel>, TaxonomyService<Rarity, RarityModel>>();
    services.AddTransient<ITaxonomyService<LanguageModel>, TaxonomyService<Language, LanguageModel>>();
    services.AddTransient<ITaxonomyService<EditionTypeModel>, TaxonomyService<EditionType, EditionTypeModel>>();
    services.AddTransient<IImportCardsService, ImportCardsService>();
    services.AddTransient<IImportCollectionService, ImportCollectionService>();
    services.AddTransient<ICardService, CardService>();
    services.AddTransient<ICardEntryService, CardEntryService>();
    services.AddTransient<IPrintService, PrintService>();
    services.AddTransient<IBinService, BinService>();
    services.AddTransient<ISectionService, SectionService>();
    // services.AddScryNet(config);
  }

  private static void AddDatabase(IServiceCollection services, IConfiguration config) =>
    services.AddDbContext<MagicCollectionContext>(
      opt => opt.UseNpgsql(
          config.GetConnectionString("MagicCollectionDb"), x => x
            .MigrationsHistoryTable("__efmigrationshistory", "public")
            .MigrationsAssembly(typeof(MagicCollectionContext).Assembly.FullName))
        .UseSnakeCaseNamingConvention(),
      ServiceLifetime.Transient);

  private static void AddAutoMapper(IServiceCollection services)
  {
    var mappingConfig = new MapperConfiguration(mc =>
    {
      mc.AddMaps("MagicCollection.Services");
    });

    var mapper = mappingConfig.CreateMapper();
    services.AddSingleton(mapper);
  }
}
using AutoMapper;
using MagicCollection.Data;
using MagicCollection.Data.Repositories;
using MagicCollection.Services.BulkData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScryNet.Extensions;

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
  }

  private static void AddServices(IServiceCollection services, IConfiguration config)
  {
    services.AddTransient<IImportCardsService, ImportCardsService>();
    services.AddTransient<IImportCollectionService, ImportCollectionService>();
    services.AddTransient<ICardService, CardService>();
    services.AddTransient<ICardEntryService, CardEntryService>();
    services.AddScryNet(config);
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
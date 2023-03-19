using MagicCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicCollection.Data;

public class MagicCollectionContext : DbContext
{
  public MagicCollectionContext(DbContextOptions<MagicCollectionContext> options) : base(options)
  {
  }
  
  public DbSet<Card> Cards { get; set; }
  public DbSet<Edition> Editions { get; set; }
  public DbSet<Language> Languages { get; set; }
  public DbSet<Print> Prints { get; set; }
  public DbSet<Rarity> Rarities { get; set; }
  public DbSet<Treatment> Treatments { get; set; }
  public DbSet<Bin> Bins { get; set; }
  public DbSet<Section> Sections { get; set; }
  public DbSet<CardEntry> CardEntries { get; set; }
  
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.HasPostgresExtension("uuid-ossp");
  }
}
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence;

using Entities;
using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Seedings;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        ConfigureDateTime(modelBuilder);
        
        modelBuilder.Seed();
    }

    private void ConfigureDateTime(ModelBuilder modelBuilder)
    {
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? v.Value.ToUniversalTime() : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.IsKeyless)
            {
                continue;
            }

            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
                else if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(nullableDateTimeConverter);
                }
            }
        }

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x is {Entity: BaseEntity, State: EntityState.Modified})
            .Select(x => x.Entity);
        foreach (var entity in entities)
        {
            if (entity is BaseEntity modified)
            {
                modified.UpdatedAt = DateTime.UtcNow;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    public DbSet<CommuneWard> CommuneWards { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<DestinationCategory> DestinationCategories { get; set; }
    public DbSet<DistrictCounty> DistrictCounties { get; set; }
    public DbSet<TravelPreference> TravelPreferences { get; set; }
    public DbSet<Itinerary> Itineraries { get; set; }
    public DbSet<ItineraryItem> ItineraryItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
}
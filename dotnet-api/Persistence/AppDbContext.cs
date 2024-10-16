namespace Persistence;

using Entities;
using Entities.Common;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
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
    public DbSet<PreferredDestination> PreferredDestinations { get; set; }
    public DbSet<TravelPreference> TravelPreferences { get; set; }
    public DbSet<User> Users { get; set; }
}
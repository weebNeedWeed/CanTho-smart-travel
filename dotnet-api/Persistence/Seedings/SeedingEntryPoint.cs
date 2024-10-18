namespace Persistence.Seedings;

using Microsoft.EntityFrameworkCore;

internal static class SeedingEntryPoint
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .SeedDistrictCounty()
            .SeedCommuneWard()
            .SeedDestinationCategory()
            .SeedDestination();
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class ItineraryConfiguration : BaseEntityConfiguration<Itinerary>
{
    public override void Configure(EntityTypeBuilder<Itinerary> builder)
    {
        base.Configure(builder);

        builder.HasMany(x => x.ItineraryItems)
            .WithOne(x => x.Itinerary)
            .HasForeignKey(x => x.ItineraryId);
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

using Entities;

public class DestinationCategoryConfiguration : BaseEntityConfiguration<DestinationCategory>
{
    public override void Configure(EntityTypeBuilder<DestinationCategory> builder)
    {
        base.Configure(builder);
        
        builder.HasMany(x => x.Destinations)
            .WithOne(x => x.DestinationCategory)
            .HasForeignKey(x => x.DestinationCategoryId);
    }
}
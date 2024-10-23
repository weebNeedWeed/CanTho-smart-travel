using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

using Entities;

public class CommuneWardConfiguration : BaseEntityConfiguration<CommuneWard>
{
    public override void Configure(EntityTypeBuilder<CommuneWard> builder)
    {
        base.Configure(builder);

        builder.HasMany(x => x.Destinations)
            .WithOne(x => x.CommuneWard)
            .HasForeignKey(x => x.CommuneWardId);
    }
}
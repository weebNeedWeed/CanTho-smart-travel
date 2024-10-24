using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

using Entities;

public class DestinationConfiguration : BaseEntityConfiguration<Destination>
{
    public override void Configure(EntityTypeBuilder<Destination> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Location)
            .HasColumnType("geometry (point)");
    }
}
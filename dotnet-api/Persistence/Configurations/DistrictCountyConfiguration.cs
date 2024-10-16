namespace Persistence.Configurations;

using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DistrictCountyConfiguration : BaseEntityConfiguration<DistrictCounty>
{
    public override void Configure(EntityTypeBuilder<DistrictCounty> builder)
    {
        base.Configure(builder);
    }
}
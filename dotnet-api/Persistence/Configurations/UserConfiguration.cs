using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

using Entities;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.TravelPreference)
            .WithOne(x => x.User)
            .HasForeignKey<TravelPreference>(x => x.UserId)
            .IsRequired();

        builder.HasMany(x => x.Itineraries)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}
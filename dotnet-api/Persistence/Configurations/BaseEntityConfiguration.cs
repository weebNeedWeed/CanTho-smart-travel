namespace Persistence.Configurations;

using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable(typeof(TEntity).Name);
        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("NOW()::timestamp");
        builder.Property(x => x.UpdatedAt)
            .HasDefaultValueSql("NOW()::timestamp");
    }
}
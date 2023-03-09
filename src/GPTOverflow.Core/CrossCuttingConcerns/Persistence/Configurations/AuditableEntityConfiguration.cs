using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;

public class AuditableEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IAuditable
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.CreatedBy).HasMaxLength(100);
        builder.Property(x => x.LastUpdatedBy).HasMaxLength(100);
    }
}
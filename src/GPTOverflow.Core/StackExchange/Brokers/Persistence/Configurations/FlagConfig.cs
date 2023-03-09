using GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;
using GPTOverflow.Core.StackExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.StackExchange.Brokers.Persistence.Configurations;

public class FlagConfig : AuditableEntityConfiguration<Flag>
{
    public override void Configure(EntityTypeBuilder<Flag> builder)
    {
        base.Configure(builder);
        builder.ToTable("flag");
        builder.Property(x => x.Category).IsRequired().HasConversion<string>();
    }
}
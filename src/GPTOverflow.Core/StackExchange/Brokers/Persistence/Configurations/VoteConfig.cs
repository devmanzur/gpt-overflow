using GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;
using GPTOverflow.Core.StackExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.StackExchange.Brokers.Persistence.Configurations;

public class VoteConfig : AuditableEntityConfiguration<Vote>
{
    public override void Configure(EntityTypeBuilder<Vote> builder)
    {
        base.Configure(builder);
        builder.ToTable("vote");
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.Type).IsRequired().HasConversion<string>().HasMaxLength(50);
    }
}
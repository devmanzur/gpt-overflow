using GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;
using GPTOverflow.Core.StackExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.StackExchange.Brokers.Persistence.Configurations;

public class ViewConfig : AuditableEntityConfiguration<View>
{
    public override void Configure(EntityTypeBuilder<View> builder)
    {
        base.Configure(builder);
        builder.ToTable("view");
        builder.Property(x => x.AccountId).IsRequired();
    }
}
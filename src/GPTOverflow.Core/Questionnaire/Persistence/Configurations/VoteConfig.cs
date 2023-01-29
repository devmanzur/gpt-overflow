using GPTOverflow.Core.Questionnaire.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.Questionnaire.Persistence.Configurations;

public class VoteConfig : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.ToTable("vote");
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.Type).IsRequired().HasConversion<string>();
    }
}
using GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;
using GPTOverflow.Core.StackExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.StackExchange.Brokers.Persistence.Configurations;

public class AnswerConfig : AuditableEntityConfiguration<Answer>
{
    public override void Configure(EntityTypeBuilder<Answer> builder)
    {
        base.Configure(builder);
        builder.ToTable("answer");
        builder.Property(x => x.Text).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.QuestionId).IsRequired();

        builder.HasMany(x => x.Comments)
            .WithOne(x=>x.Answer)
            .HasForeignKey(x => x.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Metadata.FindNavigation(nameof(Answer.Comments))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
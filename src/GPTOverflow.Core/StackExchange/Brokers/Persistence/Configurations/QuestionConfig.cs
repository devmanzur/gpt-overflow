using GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;
using GPTOverflow.Core.StackExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.StackExchange.Brokers.Persistence.Configurations;

public class QuestionConfig : AuditableEntityConfiguration<Question>
{
    public override void Configure(EntityTypeBuilder<Question> builder)
    {
        base.Configure(builder);
        builder.ToTable("question");
        builder.Property(x => x.Title).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.AccountId).HasMaxLength(500);
        builder.Property(x => x.Status).IsRequired().HasConversion<string>().HasMaxLength(50);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Question)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Views)
            .WithOne()
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Answers)
            .WithOne(x => x.Question)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Metadata.FindNavigation(nameof(Question.Comments))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Question.Views))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Question.Answers))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        
    }
}
using GPTOverflow.Core.Questionnaire.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.Questionnaire.Persistence.Configurations;

public class AnswerConfig : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable("answer");
        builder.Property(x => x.Text).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.QuestionId).IsRequired();
        
        builder.HasMany(x => x.Votes)
            .WithOne(x => x.Answer)
            .HasForeignKey(x => x.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Answer)
            .HasForeignKey(x => x.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Metadata.FindNavigation(nameof(Answer.Votes))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Answer.Comments))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
using GPTOverflow.Core.Questionnaire.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.Questionnaire.Persistence.Configurations;

public class QuestionConfig : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("question");
        builder.Property(x => x.Title).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.Status).IsRequired().HasConversion<string>();
        
                
        builder.HasMany(x => x.Votes)
            .WithOne(x => x.Question)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Question)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Views)
            .WithOne(x => x.Question)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Answers)
            .WithOne(x => x.Question)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
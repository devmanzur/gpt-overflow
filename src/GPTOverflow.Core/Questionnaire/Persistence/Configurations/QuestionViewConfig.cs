using GPTOverflow.Core.Questionnaire.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.Questionnaire.Persistence.Configurations;

public class QuestionViewConfig : IEntityTypeConfiguration<QuestionView>
{
    public void Configure(EntityTypeBuilder<QuestionView> builder)
    {
        builder.ToTable("question_view");
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.QuestionId).IsRequired();
    }
}
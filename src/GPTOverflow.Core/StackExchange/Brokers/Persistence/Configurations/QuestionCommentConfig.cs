using GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;
using GPTOverflow.Core.StackExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.StackExchange.Brokers.Persistence.Configurations;

public class QuestionCommentConfig : AuditableEntityConfiguration<QuestionComment>
{
    public override void Configure(EntityTypeBuilder<QuestionComment> builder)
    {
        base.Configure(builder);
        builder.ToTable("question_comment");
        builder.Property(x => x.Text).IsRequired();
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.QuestionId).IsRequired();
    }
}
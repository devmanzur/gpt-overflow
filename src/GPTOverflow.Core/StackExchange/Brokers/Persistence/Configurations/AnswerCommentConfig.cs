using GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;
using GPTOverflow.Core.StackExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.StackExchange.Brokers.Persistence.Configurations;

public class AnswerCommentConfig : AuditableEntityConfiguration<AnswerComment>
{
    public override void Configure(EntityTypeBuilder<AnswerComment> builder)
    {
        base.Configure(builder);
        builder.ToTable("answer_comment");
        builder.Property(x => x.Text).IsRequired();
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.AnswerId).IsRequired();
    }
}
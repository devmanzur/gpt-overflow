using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Persistence;
using GPTOverflow.Core.Questionnaire.Models;
using GPTOverflow.Core.Questionnaire.Persistence.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.Core.Questionnaire.Persistence;

public class QuestionnaireDbContext : BaseDbContext<QuestionnaireDbContext>
{
    public QuestionnaireDbContext(DbContextOptions<QuestionnaireDbContext> options,
        IDomainEventsDispatcher domainEventsDispatcher, IHttpContextAccessor httpContextAccessor) : base(options,
        domainEventsDispatcher, httpContextAccessor)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Flag> Flags { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Badge> Badges { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("questionnaire");
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AccountConfig());
        builder.ApplyConfiguration(new AnswerConfig());
        builder.ApplyConfiguration(new BadgeConfig());
        builder.ApplyConfiguration(new CommentConfig());
        builder.ApplyConfiguration(new FlagConfig());
        builder.ApplyConfiguration(new QuestionConfig());
        builder.ApplyConfiguration(new QuestionViewConfig());
        builder.ApplyConfiguration(new TagConfig());
        builder.ApplyConfiguration(new VoteConfig());

    }
}
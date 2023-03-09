using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Persistence;
using GPTOverflow.Core.StackExchange.Brokers.Persistence.Configurations;
using GPTOverflow.Core.StackExchange.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.Core.StackExchange.Brokers.Persistence;

public class StackExchangeDbContext : BaseDbContext<StackExchangeDbContext>
{
    public StackExchangeDbContext(DbContextOptions<StackExchangeDbContext> options,
        IDomainEventsDispatcher domainEventsDispatcher, IHttpContextAccessor httpContextAccessor) : base(options,
        domainEventsDispatcher, httpContextAccessor)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Flag> Flags { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Badge> Badges { get; set; }
    public DbSet<Vote> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("stack_exchange");
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AccountConfig());
        builder.ApplyConfiguration(new AnswerConfig());
        builder.ApplyConfiguration(new BadgeConfig());
        builder.ApplyConfiguration(new QuestionCommentConfig());
        builder.ApplyConfiguration(new AnswerCommentConfig());
        builder.ApplyConfiguration(new FlagConfig());
        builder.ApplyConfiguration(new QuestionConfig());
        builder.ApplyConfiguration(new ViewConfig());
        builder.ApplyConfiguration(new TagConfig());
        builder.ApplyConfiguration(new VoteConfig());
    }
}
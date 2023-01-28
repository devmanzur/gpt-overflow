using GPTOverflow.Core.Questionnaire.Models;
using GPTOverflow.Core.Shared.Contracts;
using GPTOverflow.Core.Shared.Persistence;
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
}
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StackOverflow.GPT.Core.Questionnaire.Models;
using StackOverflow.GPT.Core.Shared.Contracts;
using StackOverflow.GPT.Core.Shared.Persistence;

namespace StackOverflow.GPT.Core.Questionnaire.Persistence;

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
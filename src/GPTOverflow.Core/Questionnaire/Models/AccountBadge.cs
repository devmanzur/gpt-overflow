using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.Questionnaire.Models;

public class AccountBadge : IEntity
{
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public Guid BadgeId { get; set; }
    public Badge Badge { get; set; }
    public Guid Id { get; set; }
}
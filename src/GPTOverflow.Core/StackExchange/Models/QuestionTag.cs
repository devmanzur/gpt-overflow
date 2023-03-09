using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.StackExchange.Models;

public class QuestionTag : IEntity
{
    public Question Question { get; set; }
    public Guid QuestionId { get; set; }
    public Tag Tag { get; set; }
    public Guid TagId { get; set; }
    public Guid Id { get; set; }
}
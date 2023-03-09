using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.StackExchange.Models;

public class Vote : IEntity, IAuditable
{
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public VoteType Type { get; set; }
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
    public Guid? AnswerId { get; set; }
    public Guid? QuestionId { get; set; }

}

public enum VoteType
{
    Up,
    Down
}
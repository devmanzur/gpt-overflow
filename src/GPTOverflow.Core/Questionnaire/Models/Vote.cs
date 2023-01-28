using GPTOverflow.Core.Shared.Contracts;

namespace GPTOverflow.Core.Questionnaire.Models;

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
}

public enum VoteType
{
    Up,
    Down
}
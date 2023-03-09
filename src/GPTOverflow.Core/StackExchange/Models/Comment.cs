using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.StackExchange.Models;

public abstract class Comment : IEntity, IAuditable
{
    public string Text { get; set; }
    public Account Account { get; set; }
    public Guid AccountId { get; set; }
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
}

public class QuestionComment : Comment
{
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }
}

public class AnswerComment : Comment
{
    public Guid AnswerId { get; set; }
    public Answer Answer { get; set; }
}
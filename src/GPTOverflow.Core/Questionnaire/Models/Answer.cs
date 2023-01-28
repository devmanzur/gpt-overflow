using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.Questionnaire.Models;

public class Answer : IEntity, IAuditable
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Question Question { get; set; }
    public Guid QuestionId { get; set; }
    public Account Account { get; set; }
    public Guid AccountId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }

    public bool Accepted { get; set; }
    
    private readonly List<Comment> _comments = new List<Comment>();
    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();
    
    private readonly List<Vote> _votes = new List<Vote>();
    public IReadOnlyList<Vote> Votes => _votes.AsReadOnly();
    
}
using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.StackExchange.Models;

public enum QuestionStatus
{
    Open,
    Closed
}

public class Question : AggregateRoot, IAuditable, ISoftDeletable
{
    public string Title { get;  set; }
    public string Description { get;  set; }
    public Account Account { get; set; }
    public Guid AccountId { get; set; }
    public QuestionStatus Status { get; set; } = QuestionStatus.Open;
    public string? ClosingRemark { get;  set; }
    
    private readonly List<QuestionTag> _tags = new List<QuestionTag>();
    public IReadOnlyList<QuestionTag> Tags => _tags.AsReadOnly();
    
    private readonly List<QuestionComment> _comments = new List<QuestionComment>();
    public IReadOnlyList<QuestionComment> Comments => _comments.AsReadOnly();
    
    private readonly List<Answer> _answers = new List<Answer>();
    public IReadOnlyList<Answer> Answers => _answers.AsReadOnly();
    private readonly List<View> _views = new List<View>();
    public IReadOnlyList<View> Views => _views.AsReadOnly();
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
    public bool IsSoftDeleted { get; set; }

    public void SetTags(List<Guid> tagIds)
    {
        foreach (var tagId in tagIds)
        {
            _tags.Add(new QuestionTag()
            {
                TagId = tagId,
                QuestionId = this.Id
            });
        }
    }
}
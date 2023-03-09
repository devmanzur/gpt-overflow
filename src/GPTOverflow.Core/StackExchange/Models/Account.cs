using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.StackExchange.Models;

public class Account : AggregateRoot, IAuditable, ISoftDeletable
{
    protected Account()
    {
        
    }
    public Account(string username)
    {
        Username = username;
        Reputation = 0;
    }

    public int Reputation { get; set; }
    public string Username { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
    
    private readonly List<AccountBadge> _badges = new List<AccountBadge>();
    public IReadOnlyList<AccountBadge> Badges => _badges.AsReadOnly();
    
    private readonly List<Comment> _comments = new List<Comment>();
    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();
    
    private readonly List<Answer> _answers = new List<Answer>();
    public IReadOnlyList<Answer> Answers => _answers.AsReadOnly();
    
    private readonly List<Question> _questions = new List<Question>();
    public IReadOnlyList<Question> Questions => _questions.AsReadOnly();

    public bool IsSoftDeleted { get; set; }
}
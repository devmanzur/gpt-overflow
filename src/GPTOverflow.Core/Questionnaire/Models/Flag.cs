using GPTOverflow.Core.Shared.Contracts;

namespace GPTOverflow.Core.Questionnaire.Models;

/// <summary>
/// Flag is a way for someone to report something unacceptable in the system, the flags can be targeted at any entity
/// Such as Question, Answer, Comment or even an Account
/// </summary>
public class Flag : IEntity, IAuditable
{
    public FlagCategory Category { get; set; }
    /// <summary>
    /// Only assigned for FlagType.NeedsModeratorAttention
    /// </summary>
    public string? FlagReasonDescription { get; set; }

    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
    
    /// <summary>
    /// Foreign key relation with the parent
    /// </summary>
    public Guid? AccountId { get; set; }
    public Guid? QuestionId { get; set; }
    public Guid? AnswerId { get; set; }
    public Guid? CommentId { get; set; }

}

public enum FlagCategory
{
    Spam,
    Abusive,
    Unrelated,
    NeedsModeratorAttention
}
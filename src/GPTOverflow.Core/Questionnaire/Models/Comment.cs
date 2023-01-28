using GPTOverflow.Core.Shared.Contracts;

namespace GPTOverflow.Core.Questionnaire.Models;

public class Comment : IEntity, IAuditable
{
    public string Text { get; set; }
    public Account Account { get; set; }
    public Guid AccountId { get; set; }
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }    
    
    /// <summary>
    /// Foreign key relation with the parent
    /// </summary>
    public Guid? QuestionId { get; set; }
    public Guid? AnswerId { get; set; }
}
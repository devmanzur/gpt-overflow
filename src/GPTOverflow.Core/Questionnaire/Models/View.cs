using GPTOverflow.Core.CrossCuttinConcerns.Contracts;

namespace GPTOverflow.Core.Questionnaire.Models;

public class View : IEntity, IAuditable
{
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
}
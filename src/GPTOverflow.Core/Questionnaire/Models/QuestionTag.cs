using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.Questionnaire.Models;

public class QuestionTag : IEntity
{
    public Guid QuestionId { get; set; }
    public Guid TagId { get; set; }
    public Guid Id { get; set; }
}
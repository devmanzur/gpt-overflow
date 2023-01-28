using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.Questionnaire.Models.DataTransferObjects;

public record QuestionDto(string? Id) : BaseDto(Id)
{
    public string? Title { get; set; }
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// Last answer or comment date time
    /// </summary>
    public DateTime? LastActivity { get; set; }

    public int Votes { get; set; }
    public int Answers { get; set; }
    public int Views { get; set; }
    public List<TagDto>? Tags { get; set; }
}
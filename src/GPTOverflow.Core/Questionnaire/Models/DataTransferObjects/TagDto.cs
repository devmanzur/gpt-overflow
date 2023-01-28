using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.Questionnaire.Models.DataTransferObjects;

public record TagDto(string? Id) : BaseDto(Id)
{
    public string? Name { get; set; }
}
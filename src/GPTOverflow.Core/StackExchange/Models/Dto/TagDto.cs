using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.StackExchange.Models.Dto;

public record TagDto(string? Id) : BaseDto(Id)
{
    public string? Name { get; set; }
}
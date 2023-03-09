using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.StackExchange.Models;

public class Tag : IEntity, IAuditable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
}
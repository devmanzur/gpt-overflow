namespace StackOverflow.GPT.Core.Shared.Contracts;

public interface IAuditable
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
}
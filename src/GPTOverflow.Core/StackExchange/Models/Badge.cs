using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.StackExchange.Models;

public class Badge: IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
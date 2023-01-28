namespace GPTOverflow.Core.CrossCuttingConcerns.Contracts;

/// <summary>
/// Entities that should never be deleted from the database but instead appear as deleted in queries and views
/// should extend this one
/// </summary>
public interface ISoftDeletable
{
    public bool IsSoftDeleted { get; set; }
}
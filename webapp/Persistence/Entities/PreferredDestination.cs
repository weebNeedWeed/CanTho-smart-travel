namespace Persistence.Entities;

using Common;

public class PreferredDestination : BaseEntity
{
    public bool Visited { get; set; }
    
    public PriorityLevel PriorityLevel { get; set; }
    
    public int DestinationId { get; set; }

    public Destination Destination { get; set; } = null!;
    
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public string Notes { get; set; } = null!;
}
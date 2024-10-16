namespace Persistence.Entities;

using Common;

public class PreferredDestination : BaseEntity
{
    public bool Visited { get; set; }
    
    public PriorityLevel PriorityLevel { get; set; }
    
    public int TravelDestinationId { get; set; }

    public TravelDestination TravelDestination { get; set; } = null!;
    
    public int UserId { get; set; }

    public User User { get; set; } = null!;
}
using Persistence.Entities.Common;

namespace Persistence.Entities;

public class ItineraryItem : BaseEntity
{
    public int ItineraryId { get; set; }

    public Itinerary Itinerary { get; set; } = null!;
    
    public int DestinationId { get; set; }

    public Destination Destination { get; set; } = null!;
    
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int Priority { get; set; }

    public string? Notes { get; set; }
}
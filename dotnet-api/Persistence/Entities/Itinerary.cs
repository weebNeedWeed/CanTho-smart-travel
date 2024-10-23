using Persistence.Entities.Common;

namespace Persistence.Entities;

public class Itinerary : BaseEntity
{
    public int UserId { get; set; }

    public User User { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }

    public decimal TotalCost { get; set; }

    public ICollection<ItineraryItem> ItineraryItems { get; set; } = [];
}
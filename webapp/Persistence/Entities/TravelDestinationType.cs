namespace Persistence.Entities;

public class TravelDestinationType : BaseEntity
{
    // TouristArea, TouristSpot, GardenArea
    public int Name { get; set; }

    public string Description { get; set; } = null!;

    public ICollection<TravelDestination> TravelDestinations { get; set; } = [];
}
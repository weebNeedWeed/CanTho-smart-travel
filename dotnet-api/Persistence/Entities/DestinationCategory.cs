namespace Persistence.Entities;

using Common;

public class DestinationCategory : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public ICollection<Destination> Destinations { get; set; } = [];
}
namespace Persistence.Entities;

public class TravelDestination : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
     
    public string Address { get; set; } = null!;
    
    public string PhoneNumber { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    
    public ICollection<TravelDestinationType> TravelDestinationTypes { get; set; } = [];
    
    public int CommuneWardId { get; set; }
    
    public CommuneWard CommuneWard { get; set; } = null!;
}
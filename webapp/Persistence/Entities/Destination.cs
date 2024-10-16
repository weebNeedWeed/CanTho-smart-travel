namespace Persistence.Entities;

using Common;

public class Destination : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Description { get; set; } = null!;
    
    public string Website { get; set; } = null!;
    
    public ICollection<string> Tags { get; set; } = [];

    public ICollection<string> Amenities { get; set; } = [];
    
    public string PhoneNumber { get; set; } = null!;

    public ICollection<string> Photos { get; set; } = [];

    public Dictionary<string, string> OpeningHours { get; set; } = new(); 
    
    public string Email { get; set; } = null!;
    
    public int CommuneWardId { get; set; }

    public CommuneWard CommuneWard { get; set; } = null!;
}
namespace Persistence.Entities;

using Common;
using NetTopologySuite.Geometries;

public class Destination : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;
    
    public List<string> Tags { get; set; } = [];

    public List<string> Amenities { get; set; } = [];
    
    public string PhoneNumber { get; set; } = null!;

    public List<string> Photos { get; set; } = [];

    public Dictionary<string, string> OpeningHours { get; set; } = new(); 
    
    public string Email { get; set; } = null!;

    public Dictionary<string, string> Pricing { get; set; } = new(); 
    
    public int CommuneWardId { get; set; }

    public CommuneWard CommuneWard { get; set; } = null!;

    public Point Location { get; set; } = null!;

    public int DestinationCategoryId { get; set; }
    
    public DestinationCategory DestinationCategory { get; set; } = null!;
}
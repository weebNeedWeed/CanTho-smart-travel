namespace Persistence.Entities;

using Common;

public class CommuneWard : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public CommuneWardType Type { get; set; }
    
    public int DistrictCountyId { get; set; }

    public DistrictCounty DistrictCounty { get; set; } = null!;

    public ICollection<Destination> Destinations { get; set; } = [];
}

public enum CommuneWardType
{
    Commune,
    Ward,
    Town
}

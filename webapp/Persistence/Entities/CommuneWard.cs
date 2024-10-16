namespace Persistence.Entities;

public class CommuneWard : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public string Code { get; set; } = null!;
    
    public int DistrictCountyId { get; set; }

    public DistrictCounty DistrictCounty { get; set; } = null!;
}
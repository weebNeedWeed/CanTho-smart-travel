namespace Persistence.Entities;

using Common;

public class DistrictCounty : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public DistrictCountyType Type { get; set; }

    public ICollection<CommuneWard> CommuneWards { get; set; } = new List<CommuneWard>();
}

public enum DistrictCountyType
{
    District,
    County
}
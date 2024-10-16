namespace Persistence.Entities;

public class DistrictCounty : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public string Code { get; set; } = null!;

    public ICollection<CommuneWard> CommuneWards { get; set; } = new List<CommuneWard>();
}
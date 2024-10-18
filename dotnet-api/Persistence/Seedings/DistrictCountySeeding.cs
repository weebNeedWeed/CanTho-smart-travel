namespace Persistence.Seedings;

using Entities;
using Microsoft.EntityFrameworkCore;

internal static class DistrictCountySeeding
{
    public const int NinhKieuId = 1; 
    public const int BinhThuyId = 1; 
    public const int ThotNotId = 1; 
    public const int CaiRangId = 1; 
    public const int OMonId = 1; 
    public const int CoDoId = 1; 
    public const int PhongDienId = 1; 
    public const int ThoiLaiId = 1; 
    public const int VinhThanhId = 1; 
    
    public static ModelBuilder SeedDistrictCounty(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DistrictCounty>()
            .HasData(
                new DistrictCounty
                {
                    Name = "Ninh Kiều",
                    Type = DistrictCountyType.District
                },
                new DistrictCounty
                {
                    Name = "Bình Thuỷ",
                    Type = DistrictCountyType.District
                },
                new DistrictCounty
                {
                    Name = "Thốt Nốt",
                    Type = DistrictCountyType.District
                },
                new DistrictCounty
                {
                    Name = "Cái Răng",
                    Type = DistrictCountyType.District
                },
                new DistrictCounty
                {
                    Name = "Ô Môn",
                    Type = DistrictCountyType.District
                },
                new DistrictCounty
                {
                    Name = "Cờ Đỏ",
                    Type = DistrictCountyType.County
                },
                new DistrictCounty
                {
                    Name = "Phong Điền",
                    Type = DistrictCountyType.County
                },
                new DistrictCounty
                {
                    Name = "Thới Lai",
                    Type = DistrictCountyType.County
                },
                new DistrictCounty
                {
                    Name = "Vĩnh Thạnh",
                    Type = DistrictCountyType.County
                });
            
        return modelBuilder;
    }
}
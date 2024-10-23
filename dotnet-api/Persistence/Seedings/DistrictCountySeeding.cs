namespace Persistence.Seedings;

using Entities;
using Microsoft.EntityFrameworkCore;

internal static class DistrictCountySeeding
{
    public const int NinhKieuId = 1; 
    public const int BinhThuyId = 2; 
    public const int ThotNotId = 3; 
    public const int CaiRangId = 4; 
    public const int OMonId = 5; 
    public const int CoDoId = 6; 
    public const int PhongDienId = 7; 
    public const int ThoiLaiId = 8; 
    public const int VinhThanhId = 9; 
    
    public static ModelBuilder SeedDistrictCounty(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DistrictCounty>()
            .HasData(
                new DistrictCounty
                {
                    Id = NinhKieuId,
                    Name = "Ninh Kiều",
                    Type = DistrictCountyType.District
                },
                new DistrictCounty
                {
                    Id = BinhThuyId,
                    Name = "Bình Thuỷ",
                    Type = DistrictCountyType.District
                },
                new DistrictCounty
                {
                    Id = ThotNotId,
                    Name = "Thốt Nốt",
                    Type = DistrictCountyType.District
                },
                new DistrictCounty
                {
                    Id = CaiRangId,
                    Name = "Cái Răng",
                    Type = DistrictCountyType.District
                },
                new DistrictCounty
                {
                    Id = OMonId,
                    Name = "Ô Môn",
                    Type = DistrictCountyType.District
                },
                new DistrictCounty
                {
                    Id = CoDoId,
                    Name = "Cờ Đỏ",
                    Type = DistrictCountyType.County
                },
                new DistrictCounty
                {
                    Id = PhongDienId,
                    Name = "Phong Điền",
                    Type = DistrictCountyType.County
                },
                new DistrictCounty
                {
                    Id = ThoiLaiId,
                    Name = "Thới Lai",
                    Type = DistrictCountyType.County
                },
                new DistrictCounty
                {
                    Id = VinhThanhId,
                    Name = "Vĩnh Thạnh",
                    Type = DistrictCountyType.County
                });
            
        return modelBuilder;
    }
}
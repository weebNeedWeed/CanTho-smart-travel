using Persistence.Entities;

namespace Persistence.Seedings;

using Microsoft.EntityFrameworkCore;

internal static class DestinationCategorySeeding
{
    public const int KhuDuLichId = 1;
    public const int DiemDuLichId = 2;
    public const int DiemVuonId = 3;
    public const int AnUongId = 4;
    public const int MuaSamId = 5;
    public const int ChamSocSucKhoeId = 6;
    public const int LuuTruId = 7;
    
    public static ModelBuilder SeedDestinationCategory(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DestinationCategory>()
            .HasData(
                new DestinationCategory
                {
                    Id = KhuDuLichId,
                    Name = "Khu du lịch"
                },
                new DestinationCategory
                {
                    Id = DiemDuLichId,
                    Name = "Điểm du lịch"
                },
                new DestinationCategory
                {
                    Id = DiemVuonId,
                    Name = "Điểm vườn"
                },
                new DestinationCategory
                {
                    Id = AnUongId,
                    Name = "Ăn uống"
                },
                new DestinationCategory
                {
                    Id = MuaSamId,
                    Name = "Mua sắm"
                },
                new DestinationCategory
                {
                    Id = ChamSocSucKhoeId,
                    Name = "Chăm sóc sức khỏe"
                },
                new DestinationCategory
                {
                    Id = LuuTruId,
                    Name = "Lưu trú"
                });
        
        return modelBuilder;
    }
}
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
                new Destination
                {
                    Id = KhuDuLichId,
                    Name = "Khu du lịch"
                },
                new Destination
                {
                    Id = DiemDuLichId,
                    Name = "Điểm du lịch"
                },
                new Destination
                {
                    Id = DiemVuonId,
                    Name = "Điềm vườn"
                },
                new Destination
                {
                    Id = AnUongId,
                    Name = "Ăn uống"
                },
                new Destination
                {
                    Id = MuaSamId,
                    Name = "Mua sắm"
                },
                new Destination
                {
                    Id = ChamSocSucKhoeId,
                    Name = "Chăm sóc sức khỏe"
                },
                new Destination
                {
                    Id = LuuTruId,
                    Name = "Lưu trú"
                });
        
        return modelBuilder;
    }
}
namespace Persistence.Seedings;

using Entities;
using Microsoft.EntityFrameworkCore;

internal static class CommuneWardSeeding
{
    public const int MyKhanhId = 69;
    public const int NhonNghiaId = 70;
    public const int ThoiAnDongId = 22;
    public const int AnBinhId = 11;
    public const int TanPhuId = 33;
    
    public static ModelBuilder SeedCommuneWard(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommuneWard>()
            .HasData(GetCommuneWardList());

        return modelBuilder;
    }

    private static List<CommuneWard> GetCommuneWardList()
    {
        var communeWardList = new List<CommuneWard>
        {
            // Ninh Kieu District
            new CommuneWard
            {
                Id = 1, Name = "Cái Khế", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },
            new CommuneWard
            {
                Id = 2, Name = "An Hòa", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },
            new CommuneWard
            {
                Id = 3, Name = "Thới Bình", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },
            new CommuneWard
            {
                Id = 4, Name = "An Nghiệp", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },
            new CommuneWard
            {
                Id = 5, Name = "An Cư", Type = CommuneWardType.Ward, DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },
            new CommuneWard
            {
                Id = 6, Name = "Tân An", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },
            new CommuneWard
            {
                Id = 7, Name = "An Phú", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },
            new CommuneWard
            {
                Id = 8, Name = "Xuân Khánh", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },
            new CommuneWard
            {
                Id = 9, Name = "Hưng Lợi", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },
            new CommuneWard
            {
                Id = 10, Name = "An Khánh", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },
            new CommuneWard
            {
                Id = AnBinhId, Name = "An Bình", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.NinhKieuId
            },

            // O Mon District
            new CommuneWard
            {
                Id = 12, Name = "Châu Văn Liêm", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.OMonId
            },
            new CommuneWard
            {
                Id = 13, Name = "Thới Hòa", Type = CommuneWardType.Ward, DistrictCountyId = DistrictCountySeeding.OMonId
            },
            new CommuneWard
            {
                Id = 14, Name = "Thới Long", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.OMonId
            },
            new CommuneWard
            {
                Id = 15, Name = "Long Hưng", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.OMonId
            },
            new CommuneWard
            {
                Id = 16, Name = "Thới An", Type = CommuneWardType.Ward, DistrictCountyId = DistrictCountySeeding.OMonId
            },
            new CommuneWard
            {
                Id = 17, Name = "Phước Thới", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.OMonId
            },
            new CommuneWard
            {
                Id = 18, Name = "Trường Lạc", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.OMonId
            },

            // Binh Thuy District
            new CommuneWard
            {
                Id = 19, Name = "Bình Thủy", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.BinhThuyId
            },
            new CommuneWard
            {
                Id = 20, Name = "Trà An", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.BinhThuyId
            },
            new CommuneWard
            {
                Id = 21, Name = "Trà Nóc", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.BinhThuyId
            },
            new CommuneWard
            {
                Id = ThoiAnDongId, Name = "Thới An Đông", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.BinhThuyId
            },
            new CommuneWard
            {
                Id = 23, Name = "An Thới", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.BinhThuyId
            },
            new CommuneWard
            {
                Id = 24, Name = "Bùi Hữu Nghĩa", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.BinhThuyId
            },
            new CommuneWard
            {
                Id = 25, Name = "Long Hòa", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.BinhThuyId
            },
            new CommuneWard
            {
                Id = 26, Name = "Long Tuyền", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.BinhThuyId
            },

            // Cai Rang District
            new CommuneWard
            {
                Id = 27, Name = "Lê Bình", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.CaiRangId
            },
            new CommuneWard
            {
                Id = 28, Name = "Hưng Phú", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.CaiRangId
            },
            new CommuneWard
            {
                Id = 29, Name = "Hưng Thạnh", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.CaiRangId
            },
            new CommuneWard
            {
                Id = 30, Name = "Ba Láng", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.CaiRangId
            },
            new CommuneWard
            {
                Id = 31, Name = "Thường Thạnh", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.CaiRangId
            },
            new CommuneWard
            {
                Id = 32, Name = "Phú Thứ", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.CaiRangId
            },
            new CommuneWard
            {
                Id = TanPhuId, Name = "Tân Phú", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.CaiRangId
            },

            // Thot Not District
            new CommuneWard
            {
                Id = 34, Name = "Thốt Nốt", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.ThotNotId
            },
            new CommuneWard
            {
                Id = 35, Name = "Thới Thuận", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.ThotNotId
            },
            new CommuneWard
            {
                Id = 36, Name = "Thuận An", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.ThotNotId
            },
            new CommuneWard
            {
                Id = 37, Name = "Tân Lộc", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.ThotNotId
            },
            new CommuneWard
            {
                Id = 38, Name = "Trung Nhứt", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.ThotNotId
            },
            new CommuneWard
            {
                Id = 39, Name = "Thạnh Hòa", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.ThotNotId
            },
            new CommuneWard
            {
                Id = 40, Name = "Trung Kiên", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.ThotNotId
            },
            new CommuneWard
            {
                Id = 41, Name = "Tân Hưng", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.ThotNotId
            },
            new CommuneWard
            {
                Id = 42, Name = "Thuận Hưng", Type = CommuneWardType.Ward,
                DistrictCountyId = DistrictCountySeeding.ThotNotId
            },

            // Vinh Thanh District
            new CommuneWard
            {
                Id = 43, Name = "Vĩnh Bình", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },
            new CommuneWard
            {
                Id = 44, Name = "Thanh An", Type = CommuneWardType.Town,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },
            new CommuneWard
            {
                Id = 45, Name = "Vĩnh Thạnh", Type = CommuneWardType.Town,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },
            new CommuneWard
            {
                Id = 46, Name = "Thạnh Mỹ", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },
            new CommuneWard
            {
                Id = 47, Name = "Vĩnh Trinh", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },
            new CommuneWard
            {
                Id = 48, Name = "Thạnh An", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },
            new CommuneWard
            {
                Id = 49, Name = "Thạnh Tiến", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },
            new CommuneWard
            {
                Id = 50, Name = "Thạnh Thắng", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },
            // Vinh Thanh District (continued)
            new CommuneWard
            {
                Id = 51, Name = "Thạnh Lợi", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },
            new CommuneWard
            {
                Id = 52, Name = "Thạnh Quới", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },
            new CommuneWard
            {
                Id = 53, Name = "Thạnh Lộc", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.VinhThanhId
            },

            // Co Do District
            new CommuneWard
            {
                Id = 54, Name = "Trung An", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.CoDoId
            },
            new CommuneWard
            {
                Id = 55, Name = "Trung Thạnh", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.CoDoId
            },
            new CommuneWard
            {
                Id = 56, Name = "Thạnh Phú", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.CoDoId
            },
            new CommuneWard
            {
                Id = 57, Name = "Trung Hưng", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.CoDoId
            },
            new CommuneWard
            {
                Id = 58, Name = "Thị trấn Cờ Đỏ", Type = CommuneWardType.Town,
                DistrictCountyId = DistrictCountySeeding.CoDoId
            },
            new CommuneWard
            {
                Id = 59, Name = "Thới Hưng", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.CoDoId
            },
            new CommuneWard
            {
                Id = 60, Name = "Đông Hiệp", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.CoDoId
            },
            new CommuneWard
            {
                Id = 61, Name = "Đông Thắng", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.CoDoId
            },
            new CommuneWard
            {
                Id = 62, Name = "Thới Đông", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.CoDoId
            },
            new CommuneWard
            {
                Id = 63, Name = "Thới Xuân", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.CoDoId
            },

            // Phong Dien District
            new CommuneWard
            {
                Id = 64, Name = "Thị trấn Phong Điền", Type = CommuneWardType.Town,
                DistrictCountyId = DistrictCountySeeding.PhongDienId
            },
            new CommuneWard
            {
                Id = 65, Name = "Nhơn Ái", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.PhongDienId
            },
            new CommuneWard
            {
                Id = 66, Name = "Giai Xuân", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.PhongDienId
            },
            new CommuneWard
            {
                Id = 67, Name = "Tân Thới", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.PhongDienId
            },
            new CommuneWard
            {
                Id = 68, Name = "Trường Long", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.PhongDienId
            },
            new CommuneWard
            {
                Id = MyKhanhId, Name = "Mỹ Khánh", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.PhongDienId
            },
            new CommuneWard
            {
                Id = NhonNghiaId, Name = "Nhơn Nghĩa", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.PhongDienId
            },

            // Thoi Lai District
            new CommuneWard
            {
                Id = 71, Name = "Thị trấn Thới Lai", Type = CommuneWardType.Town,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 72, Name = "Thới Thạnh", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 73, Name = "Tân Thạnh", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 74, Name = "Xuân Thắng", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 75, Name = "Đông Bình", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 76, Name = "Đông Thuận", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 77, Name = "Thới Tân", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 78, Name = "Trường Thắng", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 79, Name = "Định Môn", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 80, Name = "Trường Thành", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 81, Name = "Trường Xuân", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 82, Name = "Trường Xuân A", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
            new CommuneWard
            {
                Id = 83, Name = "Trường Xuân B", Type = CommuneWardType.Commune,
                DistrictCountyId = DistrictCountySeeding.ThoiLaiId
            },
        };
        return communeWardList;
    }
}
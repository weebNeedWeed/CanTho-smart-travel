using Persistence.Entities;

namespace Persistence.Seedings;

using Microsoft.EntityFrameworkCore;

internal static class DestinationSeeding
{
    public static ModelBuilder SeedDestination(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Destination>()
            .HasData(
                new Destination
                {
                    Name = "Vườn du lịch Mỹ Khánh",
                    Address = "Số 335, Lộ Vòng Cung, Ấp Nhơn Mỹ, Xã Mỹ Khánh, Huyện Phong Điền, TP. Cần Thơ",
                    Description = "Vườn du lịch Mỹ Khánh là một điểm du lịch sinh thái nổi tiếng tại Cần Thơ, nơi du khách có thể trải nghiệm không gian miền Tây sông nước đặc trưng. Với diện tích rộng lớn, khu vườn kết hợp giữa thiên nhiên xanh mát và các hoạt động văn hóa truyền thống như đờn ca tài tử, tham quan nhà cổ, trải nghiệm làm chủ điền. Du khách có thể thưởng thức các loại trái cây miền nhiệt đới tươi ngon ngay tại vườn, khám phá các làng nghề truyền thống, và tham gia vào nhiều hoạt động giải trí thú vị. Đây là điểm đến lý tưởng để thư giãn và tận hưởng không gian văn hóa đậm đà bản sắc Nam Bộ.",
                    Tags = new List<string> 
                    { 
                        "Du lịch", "Sinh thái", "Văn hóa", "Miền Tây", "Cần Thơ", 
                        "Trải nghiệm", "Tham quan", "Ẩm thực", "Thiên nhiên", "Làng nghề"
                    },
                    Amenities = new List<string> 
                    { 
                        "Nhà hàng", "Khu vui chơi", "Vườn trái cây", "Làng nghề truyền thống", 
                        "Nhà cổ", "Đờn ca tài tử", "Dịch vụ thuê xe", "Khu cắm trại", 
                        "Câu cá", "Trò chơi dân gian" 
                    },
                    PhoneNumber = "0996 531 889",
                    Photos = new List<string>
                    {
                        "https://example.com/photo1.jpg",
                        "https://example.com/photo2.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ 2 - chủ nhật", "7:30 sáng - 5:00 chiều" },
                    },
                    Email = "mykhanhtourist@gmail.com"
                });

        return modelBuilder;
    }
}
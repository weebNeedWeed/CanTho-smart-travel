using NetTopologySuite.Geometries;
using Persistence.Entities;

namespace Persistence.Seedings;

using Microsoft.EntityFrameworkCore;

internal static class DestinationSeeding
{
    public static ModelBuilder SeedDestination(this ModelBuilder modelBuilder)
    {
        var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        
        modelBuilder.Entity<Destination>()
            .HasData(
                new Destination
                {
                    Id = 1,
                    Name = "Cty TNHH Du lịch Sinh Thái Mỹ Khánh",
                    Address = "335 Lộ Vòng Cung",
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
                        "mykhanh1.jpg",
                        "mykhanh2.jpg",
                        "mykhanh3.jpg",
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "7:30 sáng - 5:00 chiều" },
                    },
                    Email = "mykhanhtourist@gmail.com",
                    Location = gf.CreatePoint(new Coordinate(9.990667588558825, 105.70620329476084)),
                    CommuneWardId = CommuneWardSeeding.MyKhanhId,
                    DestinationCategoryId = DestinationCategorySeeding.KhuDuLichId
                },
                new Destination
                {
                    Id = 2,
                    Name = "Khu du lịch sinh thái Lung Cột Cầu",
                    Address = "",
                    Description = "Khu du lịch sinh thái Lung Cột Cầu là điểm đến hấp dẫn tại Cần Thơ với khung cảnh xanh mát, vườn trái cây trĩu quả và các trò chơi dân gian miền Tây đặc sắc. "
                                  + "Du khách có thể tham gia tát mương bắt cá, chèo xuồng, đi cầu khỉ và khám phá nền văn hóa Óc Eo cổ xưa. "
                                  + "Khu du lịch còn nổi tiếng với những món ăn dân dã như lẩu cua đồng, cá lóc nướng trui và lẩu mắm, mang đậm hương vị miền Tây sông nước. "
                                  + "Đây là điểm đến lý tưởng để trải nghiệm văn hóa và thiên nhiên miền Tây Nam Bộ.",
                    PhoneNumber = "0123456789",
                    Email = "info@lungcotcau.vn",
                    Tags = new List<string>
                    {
                        "Miền Tây sông nước",
                        "Du lịch sinh thái",
                        "Vườn trái cây",
                        "Trò chơi dân gian",
                        "Tát mương bắt cá",
                        "Ẩm thực miền Tây",
                        "Cần Thơ",
                        "Miệt vườn",
                        "Văn hóa Óc Eo",
                        "Hoạt động ngoài trời"
                    },
                    Amenities = new List<string>
                    {
                        "Nhà hàng",
                        "Bãi đỗ xe",
                        "Wifi miễn phí",
                        "Hướng dẫn viên",
                        "Xe điện",
                        "Khu vui chơi",
                        "Cho thuê áo bà ba",
                        "Nhà vệ sinh công cộng",
                        "Chèo thuyền",
                        "Khu nghỉ ngơi"
                    },
                    Photos = new List<string>
                    {
                        "photo1.jpg",
                        "photo2.jpg",
                        "photo3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "5:00 sáng - 5:00 chiều" },
                    },
                    Location = gf.CreatePoint(new Coordinate(9.977074365947647, 105.69742044795295)),
                    CommuneWardId = CommuneWardSeeding.NhonNghiaId,
                    DestinationCategoryId = DestinationCategorySeeding.DiemDuLichId,
                },
                new Destination
                {
                    Id = 3,
                    Name = "Làng Du Lịch Sinh Thái Ông Đề",
                    Address = "26 Ấp Mỹ Ái",
                    Description = "Làng Du Lịch Sinh Thái Ông Đề là một điểm đến nổi bật tại vùng Đồng Bằng Sông Cửu Long, mang đến trải nghiệm gần gũi với thiên nhiên. " +
                                  "Du khách có thể tận hưởng cuộc sống đồng quê đậm chất miền Tây Nam Bộ, tham gia các hoạt động dưới nước và thưởng thức đặc sản địa phương. " +
                                  "Với diện tích 3 ha, khu du lịch được bao quanh bởi vườn cây ăn trái xanh mát, nơi du khách có thể hái trái cây tươi. " +
                                  "Ngoài ra, các trò chơi dân gian như câu cá, chèo xuồng và đi cầu khỉ mang đến những giây phút vui vẻ cho mọi lứa tuổi. " +
                                  "Nơi đây còn nổi bật với văn hóa và lịch sử phong phú của vùng đất Tây Nam Bộ qua các lễ hội dân gian và sự kiện làng quê.",
                    Tags = new List<string>
                    {
                        "Du lịch sinh thái", "Đồng quê", "Miền Tây", "Thiên nhiên", "Vườn cây ăn trái",
                        "Ẩm thực địa phương", "Trò chơi dân gian", "Hoạt động dưới nước", "Phiêu lưu", "Thư giãn"
                    },
                    Amenities = new List<string>
                    {
                        "Bãi đỗ xe", "Wi-Fi miễn phí", "Nhà vệ sinh", "Hướng dẫn viên", "Nhà hàng",
                        "Cửa hàng quà lưu niệm", "Khu vui chơi trẻ em", "Khu picnic", "Chương trình văn nghệ", "Thuê xuồng"
                    },
                    PhoneNumber = "0123 456 789",
                    Photos = new List<string>
                    {
                        "hinh1.jpg", "hinh2.jpg", "hinh3.jpg", "hinh4.jpg", "hinh5.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "7:30 sáng - 5:30 chiều" },
                    },
                    Email = "lienhe@ongde.vn",
                    Location = gf.CreatePoint(new Coordinate(9.996873393488368, 105.70684051173274)),
                    CommuneWardId = CommuneWardSeeding.MyKhanhId,
                    DestinationCategoryId = DestinationCategorySeeding.DiemDuLichId,
                },
                new Destination
                {
                    Id = 4,
                    Name = "Khu Du Lịch Sinh Thái Thới An Đông",
                    Address = "Số 86 Nguyễn Văn Linh, KV Thới Thạnh",
                    Description = "Khu du lịch sinh thái Thới An Đông mang đến trải nghiệm thú vị giữa thiên nhiên xanh mát, với những căn nhà nhỏ trên cây, đồi tùng cổ thụ, và các du thuyền mini để khám phá sông nước. Du khách có thể thưởng thức ẩm thực miền Tây, tham quan vườn bonsai quý hiếm, và tận hưởng không gian yên bình đậm chất miền Tây Nam Bộ.",
                    Tags = new List<string>
                    {
                        "du lịch Cần Thơ", 
                        "khu du lịch sinh thái", 
                        "nhà trên cây", 
                        "du thuyền mini", 
                        "ẩm thực miền Tây", 
                        "vườn bonsai", 
                        "khám phá sông nước", 
                        "không gian cổ", 
                        "nghỉ dưỡng", 
                        "miền Tây Nam Bộ"
                    },
                    Amenities = new List<string>
                    {
                        "Wifi miễn phí", 
                        "Nhà hàng", 
                        "Dịch vụ du thuyền", 
                        "Nhà nghỉ trên cây", 
                        "Chỗ đậu xe miễn phí", 
                        "Cà phê Buôn Ma Thuột", 
                        "Thưởng thức đờn ca tài tử", 
                        "Chụp ảnh cổ trang", 
                        "Khu vườn bonsai", 
                        "Hoạt động trải nghiệm nông thôn"
                    },
                    PhoneNumber = "0912345678",
                    Photos = new List<string>
                    {
                        "photo1.jpg",
                        "photo2.jpg",
                        "photo3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "7:00 sáng - 6:00 chiều" },
                    },
                    Email = "info@thoianadong.com",
                    Location = gf.CreatePoint(new Coordinate(10.060284638599468, 105.70161635396583)),
                    CommuneWardId = CommuneWardSeeding.ThoiAnDongId,
                    DestinationCategoryId = DestinationCategorySeeding.DiemDuLichId,
                },
                new Destination
                {
                    Id = 5,
                    Name = "Lò Hủ Tiếu Sáu Hoài",
                    Address = "476/14 Lộ Vòng Cung",
                    Description = "Lò Hủ Tiếu Sáu Hoài là một điểm đến thú vị cho du khách khi đến Cần Thơ, nổi tiếng với quy trình làm hủ tiếu truyền thống và món Pizza Hủ Tiếu đặc sắc. Tại đây, bạn không chỉ được tham quan các công đoạn sản xuất hủ tiếu mà còn có cơ hội trải nghiệm thực tế bằng việc tự tay thử làm hủ tiếu. Nơi đây cũng phục vụ những món ăn ngon miệng như hủ tiếu xương và Pizza Hủ Tiếu. Không gian miệt vườn miền Tây tại Lò Hủ Tiếu Sáu Hoài là nơi lý tưởng để tận hưởng không khí thoáng đãng và bình yên.",
                    Tags = new List<string>
                    {
                        "Địa điểm tham quan", 
                        "Du lịch văn hóa", 
                        "Ẩm thực địa phương", 
                        "Miệt vườn", 
                        "Nghề truyền thống", 
                        "Hủ tiếu", 
                        "Cần Thơ", 
                        "Pizza Hủ Tiếu", 
                        "Chợ nổi Cái Răng", 
                        "Làng nghề"
                    },
                    Amenities = new List<string>
                    {
                        "Nhà vệ sinh", 
                        "Khu vực đỗ xe", 
                        "Nhà hàng", 
                        "Quán cà phê", 
                        "Wifi miễn phí", 
                        "Hướng dẫn viên", 
                        "Khu vực nghỉ ngơi", 
                        "Điểm check-in", 
                        "Dịch vụ homestay", 
                        "Bán đồ thủ công mỹ nghệ"
                    },
                    PhoneNumber = "0123 456 789",
                    Photos = new List<string>
                    {
                        "photo1.jpg",
                        "photo2.jpg",
                        "photo3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "6:00 sáng - 6:00 chiều" },
                    },
                    Email = "contact@sauhoai.com",
                    Location = gf.CreatePoint(new Coordinate(9.997395132932548, 105.73984463068943)),
                    CommuneWardId = CommuneWardSeeding.AnBinhId,
                    DestinationCategoryId = DestinationCategorySeeding.DiemVuonId
                },
                new Destination
                {
                    Id = 6,
                    Name = "Căn nhà màu tím",
                    Address = "Nguyễn Chí Sinh",
                    Description = "Căn nhà màu tím tọa lạc tại Cái Răng, Cần Thơ, là điểm đến lý tưởng cho những ai yêu thích màu tím. Nơi này gây ấn tượng với khung cảnh lãng mạn, từ chiếc xe đạp đến giàn hoa ti-gôn đều ngập tràn sắc tím. Bên cạnh đó, căn nhà còn lưu giữ các vật dụng của nhà Nam Bộ xưa, mang lại cảm giác hoài niệm và yên bình cho du khách. Đây không chỉ là địa điểm check-in tuyệt vời mà còn là nơi để thưởng thức không gian văn hóa độc đáo.",
                    Tags = new List<string>
                    {
                        "Màu tím", "Cần Thơ", "Cái Răng", "Điểm check-in", 
                        "Phong cách Nam Bộ", "Lãng mạn", "Hoài cổ", 
                        "Thiên nhiên", "Du lịch", "Địa điểm chụp ảnh"
                    },
                    Amenities = new List<string>
                    {
                        "Wi-Fi miễn phí", "Chỗ đậu xe", "Nhà vệ sinh sạch sẽ", 
                        "Máy lạnh", "Khu vực nghỉ ngơi", "Cửa hàng lưu niệm", 
                        "Quán cà phê", "Nước uống miễn phí", "Chụp ảnh chuyên nghiệp", 
                        "Khu vực tham quan ngoài trời"
                    },
                    PhoneNumber = "0123456789",
                    Photos = new List<string>
                    {
                        "photo1.jpg", "photo2.jpg", "photo3.jpg", "photo4.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Monday", "08:00 - 18:00" },
                        { "Tuesday", "08:00 - 18:00" },
                        { "Wednesday", "08:00 - 18:00" },
                        { "Thursday", "08:00 - 18:00" },
                        { "Friday", "08:00 - 18:00" },
                        { "Saturday", "08:00 - 18:00" },
                        { "Sunday", "08:00 - 18:00" }
                    },
                    Email = "contact@canhnhamatim.com",
                    Location = gf.CreatePoint(new Coordinate(9.970297438841989, 105.81064269258697)),
                    CommuneWardId = CommuneWardSeeding.TanPhuId,
                    DestinationCategoryId = DestinationCategorySeeding.DiemVuonId
                });

        return modelBuilder;
    }
}
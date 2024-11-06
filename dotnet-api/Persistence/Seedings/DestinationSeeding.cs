using NetTopologySuite.Geometries;
using Persistence.Entities;

namespace Persistence.Seedings;

using Microsoft.EntityFrameworkCore;

internal static class DestinationSeeding
{
    public static ModelBuilder SeedDestination(this ModelBuilder modelBuilder)
    {
        var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        
        // Seed Tourist Spots
        modelBuilder.Entity<Destination>()
            .HasData(
                new Destination
                {
                    Id = 1,
                    Name = "Cty TNHH Du lịch Sinh Thái Mỹ Khánh",
                    Address = "335 Lộ Vòng Cung",
                    Description = "Vườn du lịch Mỹ Khánh là một điểm du lịch sinh thái nổi tiếng tại Cần Thơ, nơi du khách có thể trải nghiệm không gian miền Tây sông nước đặc trưng. Với diện tích rộng lớn, khu vườn kết hợp giữa thiên nhiên xanh mát và các hoạt động văn hóa truyền thống như đờn ca tài tử, tham quan nhà cổ, trải nghiệm làm chủ điền. Du khách có thể thưởng thức các loại trái cây miền nhiệt đới tươi ngon ngay tại vườn, khám phá các làng nghề truyền thống, và tham gia vào nhiều hoạt động giải trí thú vị. Đây là điểm đến lý tưởng để thư giãn và tận hưởng không gian văn hóa đậm đà bản sắc Nam Bộ.",
                    ShortDescription = "Vườn du lịch Mỹ Khánh ở Cần Thơ là điểm du lịch sinh thái nổi tiếng, nơi du khách trải nghiệm không gian miền Tây sông nước với các hoạt động văn hóa truyền thống, thưởng thức trái cây tươi và tham gia nhiều hoạt động giải trí thú vị.",
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
                    DestinationCategoryId = DestinationCategorySeeding.KhuDuLichId,
                    Pricing =
                    {
                        { "Tham gia dự thưởng Đua heo/Đua chó", "20.000đ" },
                        { "Mồi câu cá sấu", "10.000đ" },
                        { "Khám phá Cội nguồn đất Phương Nam", "40.000đ" },
                        { "Khám phá 18 tầng địa ngục", "40.000đ" },
                        { "Bơi thuyền – đạp vịt", "50.000đ" },
                        { "Massage cá", "30.000đ" },
                        { "Tổ hợp trò chơi dân gian", "50.000đ" },
                        { "Xe điện đụng", "30.000đ" },
                    }
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
                    ShortDescription = "Khu du lịch sinh thái Lung Cột Cầu ở Cần Thơ là điểm đến hấp dẫn với cảnh quan xanh mát, vườn trái cây, các trò chơi dân gian và ẩm thực đậm chất miền Tây, mang đến trải nghiệm văn hóa và thiên nhiên độc đáo.",
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
                        "lcc1.jpg",
                        "lcc2.jpg",
                        "lcc3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "5:00 sáng - 5:00 chiều" },
                    },
                    Location = gf.CreatePoint(new Coordinate(9.977074365947647, 105.69742044795295)),
                    CommuneWardId = CommuneWardSeeding.NhonNghiaId,
                    DestinationCategoryId = DestinationCategorySeeding.DiemDuLichId,
                    Pricing =
                    {
                        { "Giá thuê áo bà ba chơi trò chơi", "30.000đ" },
                        { "Giá thuê áo bà ba chơi trò chơi (dịp lễ)", "50.000đ" },
                        { "Giá vé tham quan vườn trái cây (người lớn)", "20.000đ" },
                        { "Giá vé tham quan vườn trái cây (trẻ em)", "10.000đ" },
                        { "Giá vé chèo xuồng", "20.000đ" },
                        { "Giá món ăn tối thiểu", "10.000đ" },
                        { "Giá món ăn tối đa", "50.000đ" }
                    }
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
                    ShortDescription = "Làng Du Lịch Sinh Thái Ông Đề tại Đồng Bằng Sông Cửu Long mang đến trải nghiệm thiên nhiên miền Tây với các hoạt động dân gian, tham quan vườn cây ăn trái, và thưởng thức đặc sản địa phương.",
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
                        "od1.png", "od2.jpg", "od3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "7:30 sáng - 5:30 chiều" },
                    },
                    Email = "lienhe@ongde.vn",
                    Location = gf.CreatePoint(new Coordinate(9.996873393488368, 105.70684051173274)),
                    CommuneWardId = CommuneWardSeeding.MyKhanhId,
                    DestinationCategoryId = DestinationCategorySeeding.DiemDuLichId,
                    Pricing =
                    {
                        { "Giá vé vào cổng", "70.000đ" },
                        { "Trò chơi dân gian, teambuilding (>100 khách)", "430.000đ" },
                        { "Trò chơi dân gian, teambuilding (>50 khách)", "380.000đ" },
                        { "Tá mương bắt cá (có ăn trưa)", "360.000đ" },
                        { "Tá mương bắt cá (không ăn trưa)", "210.000đ" },
                        { "Chương trình lửa trại (đoàn >20 khách)", "330.000đ" },
                        { "Chương trình ký ức tuổi thơ (HS cấp I, đoàn >20 khách)", "175.000đ" },
                        { "Chương trình ký ức tuổi thơ (HS cấp II, đoàn >20 khách)", "195.000đ" }
                    }
                },
                new Destination
                {
                    Id = 4,
                    Name = "Khu Du Lịch Sinh Thái Thới An Đông",
                    Address = "Số 86 Nguyễn Văn Linh, KV Thới Thạnh",
                    Description = "Khu du lịch sinh thái Thới An Đông mang đến trải nghiệm thú vị giữa thiên nhiên xanh mát, với những căn nhà nhỏ trên cây, đồi tùng cổ thụ, và các du thuyền mini để khám phá sông nước. Du khách có thể thưởng thức ẩm thực miền Tây, tham quan vườn bonsai quý hiếm, và tận hưởng không gian yên bình đậm chất miền Tây Nam Bộ.",
                    ShortDescription = "Khu du lịch sinh thái Thới An Đông mang đến trải nghiệm gần gũi thiên nhiên với nhà trên cây, du thuyền mini, ẩm thực miền Tây và vườn bonsai, tạo không gian yên bình đậm chất miền Tây Nam Bộ.",
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
                        "tad1.jpg",
                        "tad2.jpg",
                        "tad3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "7:00 sáng - 6:00 chiều" },
                    },
                    Email = "info@thoianadong.com",
                    Location = gf.CreatePoint(new Coordinate(10.060284638599468, 105.70161635396583)),
                    CommuneWardId = CommuneWardSeeding.ThoiAnDongId,
                    DestinationCategoryId = DestinationCategorySeeding.DiemDuLichId,
                    Pricing =
                    {
                        { "Vé vào cổng", "50.000đ" },
                        { "Thuê nhà trên cây", "200.000đ" },
                        { "Dịch vụ du thuyền mini", "100.000đ" },
                        { "Thưởng thức ẩm thực miền Tây", "150.000đ" },
                        { "Chụp ảnh cổ trang", "80.000đ" }
                    }
                },
                new Destination
                {
                    Id = 5,
                    Name = "Lò Hủ Tiếu Sáu Hoài",
                    Address = "476/14 Lộ Vòng Cung",
                    Description = "Lò Hủ Tiếu Sáu Hoài là một điểm đến thú vị cho du khách khi đến Cần Thơ, nổi tiếng với quy trình làm hủ tiếu truyền thống và món Pizza Hủ Tiếu đặc sắc. Tại đây, bạn không chỉ được tham quan các công đoạn sản xuất hủ tiếu mà còn có cơ hội trải nghiệm thực tế bằng việc tự tay thử làm hủ tiếu. Nơi đây cũng phục vụ những món ăn ngon miệng như hủ tiếu xương và Pizza Hủ Tiếu. Không gian miệt vườn miền Tây tại Lò Hủ Tiếu Sáu Hoài là nơi lý tưởng để tận hưởng không khí thoáng đãng và bình yên.",
                    ShortDescription = "Lò Hủ Tiếu Sáu Hoài ở Cần Thơ là điểm đến hấp dẫn với quy trình làm hủ tiếu truyền thống, món Pizza Hủ Tiếu độc đáo và không gian miệt vườn yên bình.",
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
                        "haitu1.jpg",
                        "haitu2.jpg",
                        "haitu3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "6:00 sáng - 6:00 chiều" },
                    },
                    Email = "contact@sauhoai.com",
                    Location = gf.CreatePoint(new Coordinate(9.997395132932548, 105.73984463068943)),
                    CommuneWardId = CommuneWardSeeding.AnBinhId,
                    DestinationCategoryId = DestinationCategorySeeding.DiemVuonId,
                    Pricing =
                    {
                        { "Hủ tiếu pizza", "50.000đ" },
                        { "Hủ tiếu xương", "50.000đ" },
                        { "Nước sâm, sake", "10.000đ" },
                        { "Cà phê sữa đá", "20.000đ" },  
                        { "Trà đá", "5.000đ" }          
                    }
                },
                new Destination
                {
                    Id = 6,
                    Name = "Căn nhà màu tím",
                    Address = "Nguyễn Chí Sinh",
                    Description = "Căn nhà màu tím tọa lạc tại Cái Răng, Cần Thơ, là điểm đến lý tưởng cho những ai yêu thích màu tím. Nơi này gây ấn tượng với khung cảnh lãng mạn, từ chiếc xe đạp đến giàn hoa ti-gôn đều ngập tràn sắc tím. Bên cạnh đó, căn nhà còn lưu giữ các vật dụng của nhà Nam Bộ xưa, mang lại cảm giác hoài niệm và yên bình cho du khách. Đây không chỉ là địa điểm check-in tuyệt vời mà còn là nơi để thưởng thức không gian văn hóa độc đáo.",
                    ShortDescription = "Căn nhà màu tím tại Cái Răng, Cần Thơ, là điểm check-in lãng mạn cho những ai yêu sắc tím, kết hợp nét hoài niệm với không gian văn hóa Nam Bộ xưa.",
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
                        "tim1.webp", "tim2.jpg", "tim3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "6:00 sáng - 6:00 chiều" },
                    },
                    Email = "contact@canhnhamatim.com",
                    Location = gf.CreatePoint(new Coordinate(9.970297438841989, 105.81064269258697)),
                    CommuneWardId = CommuneWardSeeding.TanPhuId,
                    DestinationCategoryId = DestinationCategorySeeding.DiemVuonId,
                    Pricing =
                    {
                        { "Vé người lớn", "60.000đ" },
                        { "Vé trẻ em", "30.000đ" },
                        { "Dịch vụ gửi xe", "5.000đ" },
                        { "Thuê hướng dẫn viên", "100.000đ" },
                        { "Dịch vụ chụp ảnh", "20.000đ" }
                    }
                });

        // Hotel
        modelBuilder.Entity<Destination>()
            .HasData(
                new Destination
                {
                    Id = 7,
                    Name = "Khách sạn Sài Gòn Cần Thơ",
                    Address = "55 Phan Đình Phùng, Bến Ninh Kiều",
                    Description = "Khách sạn Sài Gòn Cần Thơ là một khách sạn 3 sao nằm tại trung tâm thành phố Cần Thơ. Được xây dựng từ năm 1996 và cải tạo vào năm 2014, khách sạn có không gian sang trọng và hiện đại, đáp ứng mọi nhu cầu lưu trú của du khách. Khách sạn có 52 phòng với các tiện nghi cao cấp như máy lạnh, truyền hình cáp, minibar và ban công riêng. Du khách cũng có thể tận hưởng trung tâm thể dục hiện đại và phòng tập yoga. Khách sạn cung cấp Wi-Fi miễn phí, dịch vụ giặt ủi, và dịch vụ nhận phòng nhanh. Đây là điểm lưu trú lý tưởng cho du khách muốn khám phá vẻ đẹp của thành phố Cần Thơ.",
                    ShortDescription = "Khách sạn Sài Gòn Cần Thơ với dịch vụ chất lượng và tiện nghi hiện đại, là lựa chọn hàng đầu cho du khách.",
                    Tags = new List<string> 
                    {
                        "Khách sạn", "Nhà hàng", "Bể bơi", "Spa", "Gym", 
                        "Dịch vụ phòng", "Giặt ủi", "Gần trung tâm", "Thân thiện gia đình", "Wifi miễn phí"
                    },
                    Amenities = new List<string>
                    {
                        "Wi-Fi miễn phí", "Truyền hình cáp", "Minibar", "Ban công riêng", "Trung tâm thể dục", 
                        "Phòng yoga", "Dịch vụ giặt là", "Dịch vụ phòng", "Nhà hàng", "Quầy lễ tân 24/7"
                    },
                    PhoneNumber = "0292 3838 123",
                    Photos = new List<string>
                    {
                        "kssg1.webp",
                        "kssg2.jpg",
                        "kssg3.webp"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "6:00 sáng - 10:00 tối" },
                    },
                    Email = "info@saigoncantho.vn",
                    Pricing = new Dictionary<string, string>
                    {
                        { "Standard Room", "600.000đ" },
                        { "Deluxe Room", "850.000đ" },
                        { "Suite Room", "1.200.000d" }
                    },
                    Location = gf.CreatePoint(new Coordinate(10.032658226562985, 105.78690793708896)),
                    CommuneWardId = CommuneWardSeeding.TanAnId,
                    DestinationCategoryId = DestinationCategorySeeding.LuuTruId,
                },
                new Destination
                {
                    Id = 8,
                    Name = "Khách sạn Phương Đông",
                    Address = "62, Đường 30 tháng 4",
                    Description = "Khách sạn Phương Đông là một khách sạn 3 sao hiện đại và tiện nghi tại trung tâm thành phố Cần Thơ. Với 50 phòng nghỉ rộng rãi, khách sạn cung cấp các tiện ích đa dạng như nhà hàng, spa, sauna và dịch vụ đưa đón. Du khách có thể dễ dàng di chuyển đến các điểm tham quan nổi tiếng của thành phố. Khách sạn cũng cung cấp các dịch vụ đặt tour, cho thuê xe, và lưu trữ hành lý để mang lại sự thuận tiện nhất cho du khách. Khách sạn Phương Đông là lựa chọn hoàn hảo cho kỳ nghỉ tại Cần Thơ.",
                    ShortDescription = "Khách sạn 3 sao hiện đại tại trung tâm Cần Thơ với đầy đủ tiện nghi và dịch vụ chất lượng.",
                    Tags = new List<string> 
                    {
                        "khách sạn", "tiện nghi", "gần trung tâm", "thoải mái", "giá hợp lý", 
                        "phòng đẹp", "dịch vụ tốt", "wifi miễn phí", "nhà hàng", "spa"
                    },
                    Amenities = new List<string>
                    {
                        "nhà hàng", "quầy bar", "phòng tập gym", "hồ bơi", "spa", 
                        "sauna", "bãi đậu xe miễn phí", "dịch vụ giặt là", "đưa đón sân bay", "wifi miễn phí"
                    },
                    PhoneNumber = "0123-456-789",
                    Photos = new List<string>
                    {
                        "kspd1.webp", "kspd2.webp", "kspd3.jpg",
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "8:00 sáng - 10:00 tối" },
                    },
                    Email = "contact@phuongdonghotel.com",
                    Pricing = new Dictionary<string, string>
                    {
                        { "Phòng đơn", "500,000đ" },
                        { "Phòng đôi", "700,000đ" },
                        { "Phòng gia đình", "1,000,000đ" }
                    },
                    Location = gf.CreatePoint(new Coordinate(10.03019074675382, 105.78016327740107)),
                    CommuneWardId = CommuneWardSeeding.TanAnId,
                    DestinationCategoryId = DestinationCategorySeeding.LuuTruId,
                },
                new Destination
                {
                    Id = 9,
                    Name = "Khách sạn Kim Thơ",
                    Address = "1 Ngô Gia Tự",
                    Description = "Khách sạn Kim Thơ là nơi nghỉ dưỡng hiện đại và tiện nghi nằm tại trung tâm thành phố Cần Thơ, bên bờ sông Hậu thơ mộng. Với dịch vụ phòng chuyên nghiệp và các tiện nghi hiện đại như spa, nhà hàng, quầy bar, khách sạn mang đến một không gian lý tưởng cho du khách muốn khám phá và nghỉ ngơi. Từ đây, bạn dễ dàng di chuyển đến các điểm du lịch nổi tiếng như Bến Ninh Kiều, Chợ nổi Cái Răng và Chùa Ông. Đội ngũ nhân viên thân thiện và chuyên nghiệp luôn sẵn sàng phục vụ 24/7, đảm bảo kỳ nghỉ đáng nhớ cho bạn.",
                    ShortDescription = "Khách sạn Kim Thơ - điểm dừng chân lý tưởng tại Cần Thơ.",
                    Tags = new List<string>
                    {
                        "Khách sạn", "Nghỉ dưỡng", "Trung tâm thành phố", "Cần Thơ", "Bến Ninh Kiều",
                        "Chợ nổi", "Dịch vụ 24/7", "Ẩm thực địa phương", "Quầy bar", "Spa thư giãn"
                    },
                    Amenities = new List<string>
                    {
                        "Wi-Fi miễn phí", "Dịch vụ phòng 24/7", "Spa", "Phòng xông hơi", "Quầy bar",
                        "Nhà hàng", "Hồ bơi", "Khu vực hút thuốc riêng", "Giặt là", "Dịch vụ xe đưa đón"
                    },
                    PhoneNumber = "0292 3736 666",
                    Photos = new List<string>
                    {
                        "kskt1.jpg",
                        "kskt2.jpg",
                        "kskt3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "6:00 sáng - 11:00 tối" },
                    },
                    Email = "info@kimthohotel.vn",
                    Pricing = new Dictionary<string, string>
                    {
                        { "Standard Room", "700,000đ" },
                        { "Deluxe Room", "1,200,000đ" },
                        { "VIP Suite", "2,500,000đ" }
                    },
                    Location = gf.CreatePoint(new Coordinate(10.035708064252358, 105.78829216931057)),
                    CommuneWardId = CommuneWardSeeding.TanAnId,
                    DestinationCategoryId = DestinationCategorySeeding.LuuTruId,
                },
                new Destination
                {
                    Id = 10,
                    Name = "Khách sạn Hậu Giang Cần Thơ",
                    Address = "34 Đ. Nam Kỳ Khởi Nghĩa",
                    Description = "Khách sạn Hậu Giang Cần Thơ là một điểm dừng chân lý tưởng cho du khách muốn khám phá vẻ đẹp miền Tây sông nước. Với vị trí thuận tiện ngay tại trung tâm thành phố, khách sạn giúp du khách dễ dàng di chuyển đến các điểm tham quan nổi tiếng như Bến Ninh Kiều, chợ nổi Cái Răng và nhiều địa danh văn hóa khác. Khách sạn được thiết kế hiện đại, cung cấp đầy đủ các tiện nghi từ nhà hàng, quán bar đến phòng họp và spa. Với đội ngũ nhân viên thân thiện, chuyên nghiệp, khách sạn đảm bảo mang lại trải nghiệm lưu trú thoải mái, tiện nghi và ấn tượng cho mỗi du khách ghé thăm.",
                    ShortDescription = "Khách sạn 3 sao với vị trí lý tưởng tại Cần Thơ, cung cấp đầy đủ tiện nghi và dịch vụ chất lượng.",
                    Tags = new List<string>
                    {
                        "Khách sạn", "Nghỉ dưỡng", "Cần Thơ", "Gần sân bay", "Nhà hàng", 
                        "Gần trung tâm", "Chợ nổi", "Bến Ninh Kiều", "Phòng họp", "Spa"
                    },
                    Amenities = new List<string>
                    {
                        "Wi-Fi miễn phí", "Nhà hàng", "Quầy bar", "Dịch vụ phòng", "Phòng họp",
                        "Spa", "Giặt là", "Trung tâm thể dục", "Bể bơi", "Bãi đậu xe miễn phí"
                    },
                    PhoneNumber = "+84 292 123 4567",
                    Photos = new List<string>
                    {
                        "kshg1.webp",
                        "kshg2.jpg",
                        "kshg3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "6:00 sáng - 11:00 tối" },
                    },
                    Email = "contact@haugianghotel.vn",
                    Pricing = new Dictionary<string, string>
                    {
                        { "Phòng đơn", "500,000đ" },
                        { "Phòng đôi", "800,000đ" },
                        { "Phòng gia đình", "1,200,000đ" },
                        { "Suite", "1,800,000đ" }
                    },
                    Location = gf.CreatePoint(new Coordinate(10.032169096391328, 105.78490699439877)),
                    CommuneWardId = CommuneWardSeeding.TanAnId,
                    DestinationCategoryId = DestinationCategorySeeding.LuuTruId,
                });
        
        // Restaurant
        modelBuilder.Entity<Destination>()
            .HasData(
                new Destination
                {
                    Id = 11,
                    Name = "Nhà Hàng Vạn Phát Cần Thơ",
                    Address = "Cồn Khương, Ninh Kiều, Cần Thơ, Việt Nam",
                    Description = "Nhà hàng Vạn Phát Cần Thơ nằm dọc sông Hậu, là nơi lý tưởng để thưởng thức ẩm thực Tây Đô với hơn 200 món ăn phong phú từ các nền ẩm thực nổi tiếng thế giới. Với không gian rộng rãi, thoáng mát, nhà hàng có khu vực A la carte, bến du thuyền và khu vui chơi trẻ em, mang đến trải nghiệm tiện nghi, thoải mái cho khách hàng. Những món đặc sản như lẩu mắm, lẩu cua đồng, cá lóc nướng và gà ướp mắm nhĩ được chế biến từ nguyên liệu tươi ngon của vùng đồng bằng sông Cửu Long, khiến thực khách không thể quên.",
                    ShortDescription = "Nơi lý tưởng để thưởng thức ẩm thực sông nước miền Tây và tận hưởng không khí sông Hậu.",
                    Tags = new List<string>
                    {
                        "Ẩm thực địa phương",
                        "Ẩm thực quốc tế",
                        "Không gian thoáng mát",
                        "Phục vụ chuyên nghiệp",
                        "View sông",
                        "Có bến du thuyền",
                        "Thực phẩm tươi ngon",
                        "Món ăn đặc sản",
                        "Phù hợp gia đình",
                        "Gần thiên nhiên"
                    },
                    Amenities = new List<string>
                    {
                        "Wi-Fi miễn phí",
                        "Bãi đỗ xe rộng",
                        "Phục vụ tại bàn",
                        "Phòng riêng",
                        "Sân chơi trẻ em",
                        "Có điều hòa",
                        "Khu vực hút thuốc",
                        "Nhà vệ sinh sạch sẽ",
                        "Khu vực bến du thuyền",
                        "An ninh đảm bảo"
                    },
                    PhoneNumber = "02923123456",
                    Photos = new List<string>
                    {
                        "vp1.png",
                        "vp2.png",
                        "vp3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "9:00 sáng - 10:00 tối" },
                    },
                    Email = "contact@vanphatrestaurant.com",
                    Pricing = new Dictionary<string, string>
                    {
                        { "Món chính", "100,000 - 300,000đ" },
                        { "Món tráng miệng", "50,000 - 150,000đ" },
                        { "Thức uống", "30,000 - 100,000đ" },
                        { "Combo gia đình", "500,000 - 1,000,000đ" }
                    },
                    Location = gf.CreatePoint(new Coordinate(10.062870510896351, 105.78323512275776)),
                    CommuneWardId = CommuneWardSeeding.CaiKheId,
                    DestinationCategoryId = DestinationCategorySeeding.AnUongId,
                },
                new Destination
                {
                    Id = 12,
                    Name = "Nhà khách số 2",
                    Address = "5 Đường Hai Bà Trưng",
                    Description = "Nhà khách số 2 tọa lạc tại vị trí đắc địa ở trung tâm Cần Thơ, gần Bến Ninh Kiều và nhiều địa điểm nổi tiếng. Khách sạn này là lựa chọn lý tưởng cho những ai muốn tận hưởng sự tiện lợi và thoải mái trong không gian yên tĩnh. Với thiết kế thân thiện, nhân viên nhiệt tình và chuyên nghiệp, khách sạn sẽ mang lại cho bạn cảm giác như ở nhà. Từ đây, bạn có thể dễ dàng khám phá các điểm tham quan du lịch và tận hưởng ẩm thực địa phương tại các nhà hàng gần đó.",
                    ShortDescription = "Khách sạn 1 sao với vị trí trung tâm và tiện ích đầy đủ, phù hợp cho du khách khám phá Cần Thơ.",
                    Tags = new List<string>
                    {
                        "Trung tâm thành phố", "Gần bến tàu", "Ẩm thực địa phương", "Thân thiện với gia đình",
                        "Dịch vụ chu đáo", "Giá hợp lý", "Thuận tiện di chuyển", "Khung cảnh đẹp",
                        "Nhiều tiện nghi", "An ninh tốt"
                    },
                    Amenities = new List<string>
                    {
                        "Wi-Fi miễn phí", "Dịch vụ phòng", "Nhà hàng", "Quán cà phê", "Máy bán hàng tự động",
                        "Dịch vụ giặt là", "Khu vực hút thuốc", "Bãi đậu xe miễn phí", "Khu vực tiếp khách chung", "Cửa hàng tiện lợi"
                    },
                    PhoneNumber = "+84 123 456 789",
                    Photos = new List<string>
                    {
                        "so21.webp", "so22.webp", "so23.webp"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "8:00 sáng - 11:00 tối" },
                    },
                    Email = "contact@nhakhachso2.com",
                    Pricing = new Dictionary<string, string>
                    {
                        { "Phòng đơn", "500,000đ" },
                        { "Phòng đôi", "700,000đ" },
                        { "Phòng gia đình", "1,200,000đ" }
                    },
                    Location = gf.CreatePoint(new Coordinate(10.029944829731878, 105.78797898465528)),
                    CommuneWardId = CommuneWardSeeding.TanAnId,
                    DestinationCategoryId = DestinationCategorySeeding.AnUongId,
                },
                new Destination
                {
                    Id = 13,
                    Name = "Nhà hàng Cây Bưởi 2",
                    Address = "Sông Hậu",
                    Description = "Nhà hàng Cây Bưởi tại Cần Thơ là một địa điểm nổi bật để khám phá ẩm thực và văn hóa miền Tây sông nước. Với không gian được thiết kế ấm cúng, sử dụng các vật liệu truyền thống, nhà hàng mang đến cảm giác gần gũi và thư thái. Thực đơn đa dạng từ các món lẩu mắm, bánh xèo, đến các món đặc sản như cá ét nướng lá chuối và chuột đồng quay lu. Đội ngũ nhân viên phục vụ tận tâm và chuyên nghiệp, đảm bảo mọi thực khách sẽ có trải nghiệm đáng nhớ. Nhà hàng cũng có các tiết mục nghệ thuật miền Tây đặc sắc.",
                    ShortDescription = "Nhà hàng Cây Bưởi - không gian ẩm thực miền Tây độc đáo tại Cần Thơ.",
                    Tags = new List<string> 
                    { 
                        "Ẩm thực miền Tây", "Không gian sang trọng", "Nhà hàng truyền thống", 
                        "Dịch vụ chuyên nghiệp", "Địa điểm tổ chức sự kiện", "Món ăn đặc sản", 
                        "Trải nghiệm văn hóa", "Thực đơn đa dạng", "Tiệc cưới", "Ẩm thực địa phương" 
                    },
                    Amenities = new List<string>
                    {
                        "Wifi miễn phí", "Chỗ đậu xe", "Phòng tiệc riêng", "Sân khấu biểu diễn", 
                        "Hệ thống âm thanh", "Điều hòa nhiệt độ", "Dịch vụ trang trí tiệc", 
                        "Chỗ ngồi ngoài trời", "Phục vụ tận nơi", "Khu vui chơi trẻ em"
                    },
                    PhoneNumber = "0292 123 4567",
                    Photos = new List<string>
                    {
                        "cb1.jpg", "cb2.jpg", "cb3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "8:00 sáng - 11:00 tối" },
                    },
                    Email = "contact@caybuoi.com",
                    Pricing = new Dictionary<string, string>
                    {
                        { "Món khai vị", "50,000 - 100,000đ" },
                        { "Món chính", "100,000 - 300,000đ" },
                        { "Món tráng miệng", "30,000 - 80,000đ" },
                        { "Nước uống", "20,000 - 60,000đ" }
                    },
                    Location = gf.CreatePoint(new Coordinate(10.04060643527213, 105.79406299661463)),
                    CommuneWardId = CommuneWardSeeding.CaiKheId,
                    DestinationCategoryId = DestinationCategorySeeding.AnUongId,
                });

        // Shopping
        modelBuilder.Entity<Destination>()
            .HasData(
                new Destination
                {
                    Id = 14,
                    Name = "Vincom Plaza Xuân Khánh",
                    Address = "209 Đường 30 Tháng 4",
                    Description = "Vincom Plaza Xuân Khánh là một trung tâm thương mại cao cấp nằm tại vị trí đắc địa tại Cần Thơ, tiếp giáp với đường 30/4 và bờ sông Cần Thơ, gần chợ nổi Cái Răng nổi tiếng. Khu tổ hợp này bao gồm tòa tháp 30 tầng, nhiều nhà hàng và khu vui chơi giải trí, thu hút hàng ngàn khách du lịch và cư dân địa phương mỗi ngày. Đây không chỉ là một địa điểm mua sắm sầm uất, mà còn là nơi thư giãn, trải nghiệm phong cách sống hiện đại với đầy đủ tiện nghi và dịch vụ chất lượng cao.",
                    ShortDescription = "Trung tâm thương mại đẳng cấp tại Cần Thơ, nơi hội tụ nhiều thương hiệu lớn và khu vui chơi giải trí.",
                    Tags = new List<string>
                    {
                        "Mua sắm", "Ẩm thực", "Giải trí", "Thời trang", "Công nghệ",
                        "Siêu thị", "Khu vui chơi", "Rạp chiếu phim", "Phòng gym", "Sang trọng"
                    },
                    Amenities = new List<string>
                    {
                        "WiFi miễn phí", "Điều hòa", "Bãi đậu xe", "Khu vực nghỉ ngơi", 
                        "ATM", "Trung tâm thông tin", "Phòng vệ sinh", "An ninh 24/7",
                        "Thang máy", "Dịch vụ hỗ trợ khách hàng"
                    },
                    PhoneNumber = "+84 292 123 4567",
                    Photos = new List<string>
                    {
                        "vxk1.jpg",
                        "vxk2.jpg",
                        "vxk3.jpg",
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "8:00 sáng - 10:00 tối" },
                    },
                    Email = "contact@vincomxk.vn",
                    Pricing = new Dictionary<string, string>
                    {
                        ["Vào cửa"] = "miễn phí"
                    },
                    Location = gf.CreatePoint(new Coordinate(10.024686369926618, 105.77505794603417)),
                    CommuneWardId = CommuneWardSeeding.XuanKhanhId,
                    DestinationCategoryId = DestinationCategorySeeding.MuaSamId,
                },
                new Destination
                {
                    Id = 15,
                    Name = "Chợ Cổ Cần Thơ",
                    Address = "Đường Hai Bà Trưng",
                    Description = "Chợ Cổ Cần Thơ là một điểm du lịch nổi tiếng, mang đậm nét văn hóa và lịch sử của vùng Nam Bộ. Được xây dựng từ năm 1915, chợ đã trải qua nhiều lần trùng tu nhưng vẫn giữ được lối kiến trúc cổ xưa. Nơi đây không chỉ là điểm tham quan hấp dẫn cho du khách mà còn là trung tâm mua sắm đa dạng với nhiều loại hàng hóa, đặc biệt là các món đồ lưu niệm và đặc sản miền Tây. Khi đến chợ, du khách sẽ cảm nhận được không khí nhộn nhịp và sự thân thiện của người dân địa phương.",
                    ShortDescription = "Chợ cổ hơn 100 năm tuổi, biểu tượng của văn hóa miền Tây Nam Bộ.",
                    Tags = new List<string>
                    {
                        "Lịch sử", "Văn hóa", "Du lịch", "Kiến trúc cổ", "Chợ", "Ẩm thực", "Đặc sản miền Tây", "Lưu niệm", "Du khách", "Tham quan"
                    },
                    Amenities = new List<string>
                    {
                        "Nhà vệ sinh công cộng", "Wifi miễn phí", "Khu vực nghỉ ngơi", "ATM", "Bãi đỗ xe", 
                        "Hướng dẫn viên", "Khu vực ăn uống", "Cửa hàng lưu niệm", "Trạm thông tin du lịch", "Dịch vụ đổi tiền"
                    },
                    PhoneNumber = "0292-123-4567",
                    Photos = new List<string>
                    {
                        "cho1.jpg", "cho2.jpg", "cho3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "8:00 sáng - 10:00 tối" },
                    },
                    Email = "contact@choCanTho.vn",
                    Pricing = new Dictionary<string, string>
                    {
                        { "Vào cổng", "Miễn phí" },
                        { "Dịch vụ hướng dẫn viên", "50.000 VND/người" },
                        { "Dịch vụ giữ xe", "5.000 VND/xe" }
                    },
                    Location = gf.CreatePoint(new Coordinate(10.0313995810151, 105.78812861127847)),
                    CommuneWardId = CommuneWardSeeding.TanAnId,
                    DestinationCategoryId = DestinationCategorySeeding.MuaSamId,
                });

        // Healthcare
        modelBuilder.Entity<Destination>()
            .HasData(
                new Destination
                {
                    Id = 16,
                    Name = "Khu nghỉ dưỡng Iris Cần Thơ",
                    Address = "224, Đ 30 tháng 4",
                    Description = "Iris Cần Thơ là một khu nghỉ dưỡng cao cấp nằm tại trung tâm thành phố Cần Thơ. Với thiết kế hiện đại kết hợp hài hòa với thiên nhiên, nơi đây cung cấp nhiều dịch vụ nghỉ dưỡng đa dạng như spa, massage, hồ bơi, và các tiện ích khác. Từ hệ thống phòng ốc sang trọng cho đến các dịch vụ thư giãn như spa, mát-xa, tắm hơi, Iris Cần Thơ hứa hẹn sẽ mang đến cho du khách một trải nghiệm nghỉ dưỡng đẳng cấp và tuyệt vời. Đội ngũ nhân viên nhiệt tình, chuyên nghiệp sẽ làm bạn hài lòng khi đến nơi đây.",
                    ShortDescription = "Khu nghỉ dưỡng sang trọng và hiện đại tại Cần Thơ.",
                    Tags = new List<string>
                    {
                        "Nghỉ dưỡng", "Cao cấp", "Spa", "Thư giãn", "Đẳng cấp", 
                        "Thiên nhiên", "Gia đình", "Massage", "Du lịch", "Phục vụ tận tâm"
                    },
                    Amenities = new List<string>
                    {
                        "Hồ bơi", "Phòng gym", "Dịch vụ spa", "Nhà hàng", "Wi-Fi miễn phí", 
                        "Bãi đỗ xe", "Phòng hội nghị", "Dịch vụ giặt ủi", "Quầy bar", "Khu vui chơi trẻ em"
                    },
                    PhoneNumber = "+84 123 456 789",
                    Photos = new List<string>
                    {
                        "ir1.jpg",
                        "ir2.jpg",
                        "ir3.jpg"
                    },
                    OpeningHours = new Dictionary<string, string>
                    {
                        { "Thứ Hai - Chủ Nhật", "8:00 sáng - 10:00 tối" },
                    },
                    Email = "contact@iriscantho.com",
                    Pricing = new Dictionary<string, string>
                    {
                        { "Dịch vụ spa", "500.000đ/giờ" },
                        { "Massage", "300.000đ/giờ" }
                    },
                    Location = gf.CreatePoint(new Coordinate(10.026980108916064, 105.77623634603417)),
                    CommuneWardId = CommuneWardSeeding.XuanKhanhId,
                    DestinationCategoryId = DestinationCategorySeeding.ChamSocSucKhoeId,
                });
        
        return modelBuilder;
    }
}
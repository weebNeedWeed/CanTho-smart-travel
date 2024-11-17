using API.Areas.Admin.Common;
using API.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Persistence;
using Persistence.Entities;

namespace API.Areas.Admin.Controllers;

[Area(AdminAreaName.Value)]
[Route("Admin/[controller]/[action]")]
public class DestinationController : Controller
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;
    public DestinationController(AppDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> AddLocation(double latitude, double longitude)
    {
        ViewBag.Quan = await _db.DistrictCounties.Select(q => new { q.Id, q.Name }).ToListAsync();
        ViewBag.Phuong = new List<object>(); // Mặc định rỗng, sẽ load qua AJAX
        ViewBag.DiaDiem = await _db.DestinationCategories.Select(q => new { q.Id, q.Name}).ToListAsync();
        ViewBag.Latitude = latitude;
        ViewBag.Longitude = longitude;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(DestinationViewModel destination)
    {
        if (ModelState.IsValid)
        {
            Destination newDes = new Destination();
            newDes.Name = destination.Name;
            newDes.Address = destination.Address;
            newDes.Description = destination.Description;
            newDes.PhoneNumber = destination.PhoneNumber;
            newDes.Email = destination.Email;
            newDes.Tags = destination.Tags;
            newDes.Amenities = destination.Amenities;
            Dictionary<string, string> TimeOpening = new Dictionary<string, string>
            {
                {"Thứ 2 - Thứ 7" , "8:00 - 22:00"}
            };
            newDes.OpeningHours = TimeOpening;
            newDes.CommuneWardId = destination.CommuneWardId;
            newDes.DestinationCategoryId = destination.DestinationCategoryId;
            newDes.Photos = new List<string>()
            {
                "no-image.jpg",
            };
            var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
           
            newDes.Location = gf.CreatePoint(new Coordinate(destination.Latitude, destination.Longitude));
            _db.Destinations.Add(newDes);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

        // Nếu lỗi, trả lại view thêm địa chỉ
        ViewBag.Quan = await _db.DistrictCounties.Select(q => new { q.Id, q.Name }).ToListAsync();
        ViewBag.Phuong = new List<object>(); // Load phường lại nếu cần
        ViewBag.DiaDiem = await _db.DestinationCategories.Select(q => new { q.Id, q.Name }).ToListAsync();
        return View("AddLocation", destination);
    }

    [HttpGet]
    public async Task<IActionResult> GetWardsByDistrict(int districtId)
    {
        var wards = await _db.CommuneWards
            .Where(w => w.DistrictCountyId == districtId)
            .Select(w => new { w.Id, w.Name })
            .ToListAsync();
        return Json(wards); // Trả về danh sách phường dưới dạng JSON
    }
}

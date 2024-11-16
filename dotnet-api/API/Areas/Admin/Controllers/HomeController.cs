namespace API.Areas.Admin.Controllers;

using API.Areas.Admin.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Persistence;

[Area(AdminAreaName.Value)]
//[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var destinations = await _db.Destinations
        .Select(d => new
        {
            d.Id,
            d.Name,
            d.Address,
            d.Description,
            d.Tags,
            d.Amenities,
            d.PhoneNumber,
            d.Photos,
            d.OpeningHours,
            d.Email,
            Latitude = d.Location.Y,
            Longitude = d.Location.X
        })
        .ToListAsync();

        return View(destinations);
    }
}
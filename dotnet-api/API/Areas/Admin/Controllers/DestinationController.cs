using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers;
public class Destination : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

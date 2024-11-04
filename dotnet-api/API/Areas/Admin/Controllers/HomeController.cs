namespace API.Areas.Admin.Controllers;

using API.Areas.Admin.Common;
using Microsoft.AspNetCore.Mvc;

[Area(AdminAreaName.Value)]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
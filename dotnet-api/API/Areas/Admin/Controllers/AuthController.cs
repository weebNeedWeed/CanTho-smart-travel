using API.Areas.Admin.Common;
using API.Areas.Admin.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Areas.Admin.Controllers;

[Area(AdminAreaName.Value)]

public class AuthController : Controller
{
    private readonly AppDbContext _db;

    public AuthController(AppDbContext db)
    {
        _db = db;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignIn(LoginViewModel model)
    {
        var admin = await _db.Admins.FirstOrDefaultAsync(x => x.Username == model.username);
        if (ModelState.IsValid)
        {
            if(admin.Username != model.username)
            {
                ModelState.AddModelError(string.Empty, "Tên đăng nhập sai.");
                return View(model);
            }
            if(admin.Password != model.password)
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu sai.");
                return View(model);
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        return View(model);
    }
}
 
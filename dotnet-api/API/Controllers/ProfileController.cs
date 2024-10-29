using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using API.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProfileController: ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ProfileController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [Route("itineraries")]
    [HttpGet]
    public async Task<IActionResult> GetAllItineraries()
    {
        var userId = int.Parse(HttpContext.User.Claims
            .First(x => x.Type.Equals(ClaimTypes.Sid))
            .Value);

        var itineraries = await _appDbContext.Itineraries
            .Where(x => x.UserId.Equals(userId))
            .ToListAsync();

        return Ok(itineraries);
    }
    
    [Route("settings")]
    [HttpGet]
    public async Task<IActionResult> GetAllSettings()
    {
        var userId = int.Parse(HttpContext.User.Claims
            .First(x => x.Type.Equals(ClaimTypes.Sid))
            .Value);

        var userPreferences = await _appDbContext.TravelPreferences
            .FirstOrDefaultAsync(x => x.UserId == userId);
        if (userPreferences is null)
        {
            userPreferences = new TravelPreference
            {
                UserId = userId,
                BudgetMin = 0.00m,
                BudgetMax = 0.00m,
                PreferenceTags = []
            };
            await _appDbContext.TravelPreferences.AddAsync(userPreferences);
            await _appDbContext.SaveChangesAsync();
        }

        return Ok(userPreferences);
    }

    [Route("settings")]
    [HttpPost]
    public async Task<IActionResult> SaveSettings([FromBody]SaveSettingsRequest request)
    {
        var userId = int.Parse(HttpContext.User.Claims
            .First(x => x.Type.Equals(ClaimTypes.Sid))
            .Value);

        var userPreferences = await _appDbContext.TravelPreferences
            .FirstOrDefaultAsync(x => x.UserId == userId);
        
        if (userPreferences is null)
        {
            userPreferences = new TravelPreference
            {
                UserId = userId,
                BudgetMin = 0.00m,
                BudgetMax = 0.00m,
                PreferenceTags = []
            };
            await _appDbContext.TravelPreferences.AddAsync(userPreferences);
        }

        userPreferences.BudgetMin = request.BudgetMin;
        userPreferences.BudgetMax = request.BudgetMax;
        userPreferences.PreferenceTags = request.Tags.ToList();

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}
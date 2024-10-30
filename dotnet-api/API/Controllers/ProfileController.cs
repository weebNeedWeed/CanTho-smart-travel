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
            .Include(x => x.ItineraryItems)
            .ThenInclude(x => x.Destination)
            .Where(x => x.UserId.Equals(userId))
            .ToListAsync();

        return Ok(itineraries);
    }

    [Route("itineraries")]
    [HttpPost]
    public async Task<IActionResult> CreateItineraryWithDefaultOptions()
    {
        var userId = int.Parse(HttpContext.User.Claims
            .First(x => x.Type.Equals(ClaimTypes.Sid))
            .Value);

        var newItinerary = new Itinerary
        {
            UserId = userId,
            TotalCost = 0m,
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = DateOnly.FromDateTime(DateTime.Now),
            Name = "Lịch trình mặc định",
            ItineraryItems = [],
        };

        await _appDbContext.AddAsync(newItinerary);
        await _appDbContext.SaveChangesAsync();

        return Ok(newItinerary);
    }
    
    [Route("itineraries/{id:int}")]
    [HttpPatch]
    public async Task<IActionResult> UpdateItinerary([FromRoute] int id, [FromBody]UpdateItineraryRequest request)
    {
        var newItinerary = await _appDbContext.Itineraries
            .FirstAsync(x => x.Id == id);

        _appDbContext.ItineraryItems.RemoveRange(
            await _appDbContext.ItineraryItems
                .Where(x => x.ItineraryId == id).ToListAsync());
        
        newItinerary.ItineraryItems = [];
        if (request.ItineraryItems is not null && request.ItineraryItems.Length > 0)
        {
            foreach (var item in request.ItineraryItems)
            {
                newItinerary.ItineraryItems.Add(new ItineraryItem
                {
                    DestinationId = item.DestinationId,
                    Notes = item.Notes,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Priority = item.Priority,
                });
            }
        }

        newItinerary.Name = request.Name;
        newItinerary.TotalCost = request.TotalCost;
        newItinerary.StartDate = request.StartDate;
        newItinerary.EndDate = request.EndDate;
        
        await _appDbContext.SaveChangesAsync();

        return Ok(newItinerary);
    }

    [Route("itineraries/{id:int}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteItinerary([FromRoute] int id)
    {
        var iti = await _appDbContext.Itineraries
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (iti is null)
        {
            return BadRequest();
        }

        _appDbContext.Itineraries.Remove(iti);
        await _appDbContext.SaveChangesAsync();

        return Ok();
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
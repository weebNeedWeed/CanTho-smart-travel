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
    public async Task<IActionResult> CreateItinerary([FromBody]CreateItineraryRequest request)
    {
        var userId = int.Parse(HttpContext.User.Claims
            .First(x => x.Type.Equals(ClaimTypes.Sid))
            .Value);

        var newItinerary = new Itinerary
        {
            UserId = userId,
            TotalCost = request.TotalCost,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Name = request.Name,
            ItineraryItems = [],
        };

        if (request.CreateItineraryItemRequests is not null && request.CreateItineraryItemRequests.Length > 0)
        {
            foreach (var item in request.CreateItineraryItemRequests)
            {
                newItinerary.ItineraryItems.Add(new ItineraryItem
                {
                    DestinationId = item.DestinationId,
                    Notes = item.Notes,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Priority = item.Priority
                });
            }
        }

        await _appDbContext.AddAsync(newItinerary);
        await _appDbContext.SaveChangesAsync();

        return Ok(newItinerary);
    }
    
    [Route("itineraries")]
    [HttpPatch]
    public async Task<IActionResult> UpdateItinerary([FromBody]UpdateItineraryRequest request)
    {
        var newItinerary = await _appDbContext.Itineraries
            .FirstAsync(x => x.Id == request.ItineraryId);

        if (request.UpdateItineraryItemRequests is not null && request.UpdateItineraryItemRequests.Length > 0)
        {
            foreach (var item in request.UpdateItineraryItemRequests)
            {
                var itiItem = await _appDbContext.ItineraryItems
                    .FirstOrDefaultAsync(x => x.Id == item.ItineraryItemId);
                if (itiItem is null)
                {
                    newItinerary.ItineraryItems.Add(new ItineraryItem
                    {
                        DestinationId = item.DestinationId,
                        Notes = item.Notes,
                        StartTime = item.StartTime,
                        EndTime = item.EndTime,
                        Priority = item.Priority
                    });
                    continue;
                }

                itiItem.DestinationId = item.DestinationId;
                itiItem.Notes = item.Notes;
                itiItem.StartTime = item.StartTime;
                itiItem.EndTime = item.EndTime;
                itiItem.Priority = item.Priority;
            }
        }
        
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
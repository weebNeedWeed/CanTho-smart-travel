using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Persistence;
using Persistence.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DestinationsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public DestinationsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDests()
    {
        var dests = await _appDbContext.Destinations.ToListAsync();
        return Ok(dests);
    }
    
    [HttpGet(":geojson")]
    public async Task<IActionResult> GetAllDestAsGeoJson()
    {
        var featureCollection = new FeatureCollection();
        
        var dests = await _appDbContext
            .Destinations
            .Include(x => x.DestinationCategory)
            .ToListAsync();
        dests.ForEach(dest =>
        {
            var attributesTable = new AttributesTable();
            attributesTable.Add(nameof(Destination.Name), dest.Name);
            attributesTable.Add(nameof(Destination.DestinationCategoryId), dest.DestinationCategoryId);
            attributesTable.Add("DestinationCategoryName", dest.DestinationCategory.Name);
            var feature = new Feature(dest.Location, attributesTable);
            featureCollection.Add(feature);
        });
        return Ok(featureCollection);
    }
    
    [Route("/api/categories")]
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var dests = await _appDbContext
            .DestinationCategories
            .ToListAsync();
        var responseDest = dests
            .Select(x => new CategoryGetAllResponse(x.Id, x.Name))
            .ToList();
        return Ok(responseDest);
    }
}
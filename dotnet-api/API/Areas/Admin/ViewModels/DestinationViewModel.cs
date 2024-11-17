using NetTopologySuite.Geometries;
using Persistence.Entities;

namespace API.Areas.Admin.ViewModels;

public class DestinationViewModel
{
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public List<string> Tags { get; set; } = [];

    public List<string> Amenities { get; set; } = [];

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int CommuneWardId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int DestinationCategoryId { get; set; }
}


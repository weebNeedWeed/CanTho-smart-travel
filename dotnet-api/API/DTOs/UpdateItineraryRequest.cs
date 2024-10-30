namespace API.DTOs;

public class UpdateItineraryRequest
{
    public string Name { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }

    public decimal? TotalCost { get; set; }

    public UpdateItineraryItemRequest[]? ItineraryItems { get; set; } = [];
}

public class UpdateItineraryItemRequest
{
    public int DestinationId { get; set; }
    
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int Priority { get; set; }

    public string? Notes { get; set; }
}
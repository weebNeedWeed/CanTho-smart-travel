namespace API.DTOs;

public class UpdateItineraryRequest
{
    public int ItineraryId { get; set; }
    
    public string Name { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }

    public decimal? TotalCost { get; set; }

    public UpdateItineraryItemRequest[]? UpdateItineraryItemRequests { get; set; } = [];
}

public class UpdateItineraryItemRequest
{
    public int ItineraryItemId { get; set; }
    
    public int DestinationId { get; set; }
    
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int Priority { get; set; }

    public string? Notes { get; set; }
}
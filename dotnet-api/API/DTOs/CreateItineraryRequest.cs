namespace API.DTOs;

public class CreateItineraryRequest
{
    public string Name { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }

    public decimal? TotalCost { get; set; }

    public CreateItineraryItemRequest[]? CreateItineraryItemRequests { get; set; } = [];
}

public class CreateItineraryItemRequest
{
    public int DestinationId { get; set; }
    
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int Priority { get; set; }

    public string? Notes { get; set; }
}
namespace Persistence.Entities;

public class TravelPreferences : BaseEntity
{
    public decimal BudgetMin { get; set; }
    
    public decimal BudgetMax { get; set; }
    
    public ICollection<TravelDestinationType> TravelDestinationTypes { get; set; } = [];
    
    public int UserId { get; set; }

    public User User { get; set; } = null!;
}
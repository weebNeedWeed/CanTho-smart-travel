namespace Persistence.Entities;

using Common;

public class TravelPreferences : BaseEntity
{
    public decimal BudgetMin { get; set; }
    
    public decimal BudgetMax { get; set; }

    public ICollection<string> PreferenceTags { get; set; } = [];
    
    public int UserId { get; set; }

    public User User { get; set; } = null!;
}
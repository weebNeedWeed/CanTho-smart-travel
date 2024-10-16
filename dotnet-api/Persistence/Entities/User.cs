namespace Persistence.Entities;

using Common;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public ICollection<PreferredDestination> PreferredDestinations { get; set; } = [];
    
    public ICollection<TravelPreferences> TravelPreferences { get; set; } = [];
}
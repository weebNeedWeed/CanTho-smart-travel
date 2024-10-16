namespace Persistence.Entities;

using Common;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
    
}
namespace Inventory.Client.Models.Auth;

public class AppUser
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public RoleType RoleType { get; set; }
}
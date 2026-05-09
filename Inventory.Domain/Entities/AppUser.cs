using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities;

public class AppUser
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public int AppRoleId { get; set; }
    public AppRole? Role { get; set; }
}
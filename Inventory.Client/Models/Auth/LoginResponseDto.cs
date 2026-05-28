namespace Inventory.Client.Models.Auth;

public class LoginResponseDto
{
    public bool IsAuthenticated { get; set; }

    public AppUser? User { get; set; }

    public string? ErrorMessage { get; set; }
}
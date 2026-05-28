using Inventory.Client.Models.Auth;

namespace Inventory.Client.Services.Auth;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);

    Task LogoutAsync();

    Task<AppUser?> GetCurrentUserAsync();

    bool IsAuthenticated { get; }
}
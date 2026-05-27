using System.Text.Json;
using Microsoft.JSInterop;
using Inventory.Client.Models.Auth;

namespace Inventory.Client.Services.Auth;

public class AuthService : IAuthService
{
    private const string SessionKey = "auth_user";

    private readonly IJSRuntime _js;

    private AppUser? _currentUser;

    public AuthService(
        IJSRuntime js)
    {
        _js = js;
    }

    public bool IsAuthenticated =>
        _currentUser is not null;

    public async Task<LoginResponseDto> LoginAsync(
        LoginRequestDto request)
    {
        if (request.Username == "owner" &&
            request.Password == "owner123")
        {
            _currentUser = new AppUser
            {
                Id = 1,
                FirstName = "Demo",
                LastName = "Owner",
                IsActive = true,
                RoleType = RoleType.Owner
            };

            await SaveUserSessionAsync();

            return new LoginResponseDto
            {
                IsAuthenticated = true,
                User = _currentUser
            };
        }

        if (request.Username == "cashier" &&
            request.Password == "cashier123")
        {
            _currentUser = new AppUser
            {
                Id = 2,
                FirstName = "Demo",
                LastName = "Cashier",
                IsActive = true,
                RoleType = RoleType.Cashier
            };

            await SaveUserSessionAsync();

            return new LoginResponseDto
            {
                IsAuthenticated = true,
                User = _currentUser
            };
        }

        if (request.Username == "clerk" &&
            request.Password == "clerk123")
        {
            _currentUser = new AppUser
            {
                Id = 3,
                FirstName = "Stock",
                LastName = "Clerk",
                IsActive = true,
                RoleType = RoleType.StockClerk
            };

            await SaveUserSessionAsync();

            return new LoginResponseDto
            {
                IsAuthenticated = true,
                User = _currentUser
            };
        }

        return new LoginResponseDto
        {
            IsAuthenticated = false,
            ErrorMessage = "Invalid username or password."
        };
    }

    public async Task LogoutAsync()
    {
        _currentUser = null;

        await _js.InvokeVoidAsync(
            "sessionStorage.removeItem",
            SessionKey);
    }

    public async Task<AppUser?> GetCurrentUserAsync()
    {
        if (_currentUser is not null)
            return _currentUser;

        var json =
            await _js.InvokeAsync<string?>(
                "sessionStorage.getItem",
                SessionKey);

        if (string.IsNullOrWhiteSpace(json))
            return null;

        _currentUser =
            JsonSerializer.Deserialize<AppUser>(
                json);

        return _currentUser;
    }

    private async Task SaveUserSessionAsync()
    {
        var json =
            JsonSerializer.Serialize(
                _currentUser);

        await _js.InvokeVoidAsync(
            "sessionStorage.setItem",
            SessionKey,
            json);
    }
}
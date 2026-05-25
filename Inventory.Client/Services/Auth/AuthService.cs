using Inventory.Client.Models.Auth;

namespace Inventory.Client.Services.Auth;

public class AuthService : IAuthService
{
    private AppUser? _currentUser;

    public bool IsAuthenticated => _currentUser is not null;

    public Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        if (request.Username == "owner" && request.Password == "owner123")
        {
            _currentUser = new AppUser
            {
                Id = 1,
                FirstName = "Demo",
                LastName = "Owner",
                IsActive = true,
                RoleType = RoleType.Owner
            };

            return Task.FromResult(new LoginResponseDto
            {
                IsAuthenticated = true,
                User = _currentUser
            });
        }

        if (request.Username == "cashier" && request.Password == "cashier123")
        {
            _currentUser = new AppUser
            {
                Id = 2,
                FirstName = "Demo",
                LastName = "Cashier",
                IsActive = true,
                RoleType = RoleType.Cashier
            };

            return Task.FromResult(new LoginResponseDto
            {
                IsAuthenticated = true,
                User = _currentUser
            });
        }

        if (request.Username == "clerk" && request.Password == "clerk123")
        {
            _currentUser = new AppUser
            {
                Id = 3,
                FirstName = "Stock",
                LastName = "Clerk",
                IsActive = true,
                RoleType = RoleType.StockClerk
            };

            return Task.FromResult(new LoginResponseDto
            {
                IsAuthenticated = true,
                User = _currentUser
            });
        }

        return Task.FromResult(new LoginResponseDto
        {
            IsAuthenticated = false,
            ErrorMessage = "Invalid username or password."
        });
    }

    public Task LogoutAsync()
    {
        _currentUser = null;
        return Task.CompletedTask;
    }

    public Task<AppUser?> GetCurrentUserAsync()
    {
        return Task.FromResult(_currentUser);
    }
}
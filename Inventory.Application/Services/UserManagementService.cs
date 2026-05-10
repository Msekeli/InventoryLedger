using Inventory.Domain.Entities;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Services;

public class UserManagementService : IUserManagementService
{
    public Task<AppUser?> GetByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<AppUser>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(AppUser user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(AppUser user)
    {
        throw new NotImplementedException();
    }

    public Task DeactivateAsync(int userId)
    {
        throw new NotImplementedException();
    }
}
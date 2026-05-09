using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IUserManagementService
{
    Task<AppUser?> GetByIdAsync(int userId);

    Task<List<AppUser>> GetAllAsync();

    Task AddAsync(AppUser user);

    Task UpdateAsync(AppUser user);

    Task DeactivateAsync(int userId);
}
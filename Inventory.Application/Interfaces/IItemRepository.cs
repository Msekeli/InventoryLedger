using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(int id);
    Task<Item?> GetBySKUAsync(string sku);
    Task<List<Item>> GetAllAsync();
    Task AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(Item item);
    Task<bool> SaveChangesAsync();
}

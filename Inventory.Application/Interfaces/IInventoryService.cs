using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryService
{
    Task<int> GetOnHandQuantityAsync(int itemId);
    Task<decimal> GetInventoryValueAsync(int itemId);
    Task<decimal> GetTotalInventoryValueAsync();
    Task<List<Item>> GetLowStockItemsAsync();
}

using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryService
{
    Task<int> GetOnHandQuantityAsync(int itemId);

    Task<decimal> GetInventoryValueAsync(int itemId);

    Task<decimal> GetTotalInventoryValueAsync();

    Task<List<Item>> GetLowStockItemsAsync();

    Task ReceiveStockAsync(int itemId, int quantity, int supplierId, int performedByUserId);

    Task RecordDamageAsync(int itemId, int quantity, int performedByUserId);

    Task WriteOffExpiredStockAsync(int itemId, int quantity, int performedByUserId);

    Task AdjustStockAsync(int itemId, int quantity, int performedByUserId);

    Task PerformStockCountAsync(int itemId, int countedQuantity, int performedByUserId);
}
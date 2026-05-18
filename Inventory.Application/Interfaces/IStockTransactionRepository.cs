using Inventory.Domain.Entities;
using Inventory.Application.Models;

namespace Inventory.Application.Interfaces;

public interface IStockTransactionRepository
{
    Task<StockTransaction?> GetByIdAsync(int id);

    Task<List<StockTransaction>> GetByItemIdAsync(int itemId);

    Task<List<StockTransaction>> GetAllAsync();

    Task ReceiveStockAsync(
        int itemId,
        int quantity,
        string referenceNumber,
        string notes,
        int performedByUserId);

    Task ProcessSaleAsync(
        int itemId,
        int quantity,
        string referenceNumber,
        string notes,
        int performedByUserId);

    Task<int> GetOnHandQuantityAsync(int itemId);

    Task<List<InventorySummaryRow>> GetInventorySummaryAsync();

    Task<List<LowStockItemRow>> GetLowStockItemsAsync();

    Task<bool> SaveChangesAsync();
}
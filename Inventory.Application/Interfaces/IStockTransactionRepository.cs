using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IStockTransactionRepository
{
    Task<List<StockTransaction>> GetByItemIdAsync(int itemId);
    Task AddAsync(StockTransaction transaction);
    Task<bool> SaveChangesAsync();
}

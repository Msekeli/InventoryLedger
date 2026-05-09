using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IStockTransactionRepository
{
    Task<StockTransaction?> GetByIdAsync(int id);

    Task<List<StockTransaction>> GetByItemIdAsync(int itemId);

    Task<List<StockTransaction>> GetAllAsync();

    Task AddAsync(StockTransaction transaction);

    Task<bool> SaveChangesAsync();
}
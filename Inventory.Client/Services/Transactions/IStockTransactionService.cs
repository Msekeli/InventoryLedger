using Inventory.Client.Models.Transactions;

namespace Inventory.Client.Services.Transactions
{
    public interface IStockTransactionService
    {
        Task<List<StockTransactionDto>> GetAllAsync();
        Task<bool> CreateAsync(CreateStockTransactionDto model);
        Task<List<StockTransactionDto>> GetByItemIdAsync(int itemId);
    }
}

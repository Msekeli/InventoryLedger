using Inventory.Client.Models.Transactions;

namespace Inventory.Client.Services.Transactions
{
    public interface IStockTransactionService
    {
        Task<bool> CreateAsync(CreateStockTransactionDto dto);
        Task<List<StockTransactionDto>> GetAllAsync();
    }
}

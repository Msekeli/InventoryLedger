using Inventory.Domain.Entities;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Services;

public class InventoryService : IInventoryService
{
    private readonly IItemRepository _itemRepository;
    private readonly IStockTransactionRepository _transactionRepository;

    public InventoryService(
        IItemRepository itemRepository,
        IStockTransactionRepository transactionRepository)
    {
        _itemRepository = itemRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<int> GetOnHandQuantityAsync(int itemId)
    {
        var transactions = await _transactionRepository.GetByItemIdAsync(itemId);

        return transactions.Sum(t => t.QuantityChange);
    }

    public async Task<decimal> GetInventoryValueAsync(int itemId)
    {
        var item = await _itemRepository.GetByIdAsync(itemId);
        if (item == null)
            return 0;

        var onHand = await GetOnHandQuantityAsync(itemId);

        return item.UnitPrice * onHand;
    }

    public async Task<decimal> GetTotalInventoryValueAsync()
    {
        var items = await _itemRepository.GetAllAsync();
        decimal total = 0;

        foreach (var item in items)
        {
            var value = await GetInventoryValueAsync(item.Id);
            total += value;
        }

        return total;
    }

    public async Task<List<Item>> GetLowStockItemsAsync()
    {
        var items = await _itemRepository.GetAllAsync();
        var lowStock = new List<Item>();

        foreach (var item in items)
        {
            var onHand = await GetOnHandQuantityAsync(item.Id);

            if (item.IsLowStock(onHand))
            {
                lowStock.Add(item);
            }
        }

        return lowStock;
    }
}

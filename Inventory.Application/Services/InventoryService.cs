using Inventory.Domain.Entities;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Services;

public class InventoryService
    : IInventoryService
{
    private readonly
        IItemRepository _itemRepository;

    private readonly
        IStockTransactionRepository
        _transactionRepository;

    public InventoryService(
        IItemRepository itemRepository,
        IStockTransactionRepository
            transactionRepository)
    {
        _itemRepository =
            itemRepository;

        _transactionRepository =
            transactionRepository;
    }

    public async Task<int>
        GetOnHandQuantityAsync(
            int itemId)
    {
        return await
            _transactionRepository
                .GetOnHandQuantityAsync(
                    itemId);
    }

    public async Task<decimal>
        GetInventoryValueAsync(
            int itemId)
    {
        var item =
            await _itemRepository
                .GetByIdAsync(
                    itemId);

        if (item is null)
            return 0;

        var onHand =
            await GetOnHandQuantityAsync(
                itemId);

        return item.CostPrice
            * onHand;
    }

    public async Task<decimal>
        GetTotalInventoryValueAsync()
    {
        var items =
            await _itemRepository
                .GetAllAsync();

        decimal total = 0;

        foreach (
            var item
            in items)
        {
            total +=
                await GetInventoryValueAsync(
                    item.Id);
        }

        return total;
    }

    public async Task<List<Item>>
        GetLowStockItemsAsync()
    {
        var items =
            await _itemRepository
                .GetActiveItemsAsync();

        var lowStockItems =
            new List<Item>();

        foreach (
            var item
            in items)
        {
            var onHand =
                await GetOnHandQuantityAsync(
                    item.Id);

            if (
                item.IsLowStock(
                    onHand))
            {
                lowStockItems
                    .Add(item);
            }
        }

        return lowStockItems;
    }

    public Task ReceiveStockAsync(
        int itemId,
        int quantity,
        int supplierId,
        int performedByUserId)
    {
        throw new NotImplementedException();
    }

    public Task RecordDamageAsync(
        int itemId,
        int quantity,
        int performedByUserId)
    {
        throw new NotImplementedException();
    }

    public Task WriteOffExpiredStockAsync(
        int itemId,
        int quantity,
        int performedByUserId)
    {
        throw new NotImplementedException();
    }

    public Task AdjustStockAsync(
        int itemId,
        int quantity,
        int performedByUserId)
    {
        throw new NotImplementedException();
    }

    public Task PerformStockCountAsync(
        int itemId,
        int countedQuantity,
        int performedByUserId)
    {
        throw new NotImplementedException();
    }
}
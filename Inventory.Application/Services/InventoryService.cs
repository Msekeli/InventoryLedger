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
        var summary =
            await _transactionRepository
                .GetInventorySummaryAsync();

        var item =
            summary
                .FirstOrDefault(
                    x => x.Id == itemId);

        if (item is null)
            return 0;

        return item
            .InventoryCostValue;
    }

    public async Task<decimal>
        GetTotalInventoryValueAsync()
    {
        var summary =
            await _transactionRepository
                .GetInventorySummaryAsync();

        return summary
            .Sum(
                x => x.InventoryCostValue);
    }

    public async Task<List<Item>>
        GetLowStockItemsAsync()
    {
        var lowStockRows =
            await _transactionRepository
                .GetLowStockItemsAsync();

        var items =
            await _itemRepository
                .GetActiveItemsAsync();

        return items
            .Where(
                i => lowStockRows
                    .Any(
                        x => x.Id == i.Id))
            .ToList();
    }

    public async Task
        ReceiveStockAsync(
            int itemId,
            int quantity,
            int supplierId,
            int performedByUserId)
    {
        await _transactionRepository
            .ReceiveStockAsync(
                itemId,
                quantity,
                $"REC-{itemId}",
                "Inventory service",
                performedByUserId);
    }

    public Task
        RecordDamageAsync(
            int itemId,
            int quantity,
            int performedByUserId)
    {
        throw new NotImplementedException();
    }

    public Task
        WriteOffExpiredStockAsync(
            int itemId,
            int quantity,
            int performedByUserId)
    {
        throw new NotImplementedException();
    }

    public Task
        AdjustStockAsync(
            int itemId,
            int quantity,
            int performedByUserId)
    {
        throw new NotImplementedException();
    }

    public Task
        PerformStockCountAsync(
            int itemId,
            int countedQuantity,
            int performedByUserId)
    {
        throw new NotImplementedException();
    }
}
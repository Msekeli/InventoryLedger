using Inventory.Application.Interfaces;
using Inventory.Api.Models.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController
    : ControllerBase
{
    private readonly
        IInventoryService
        _inventoryService;

    private readonly
        IItemRepository
        _itemRepository;

    private readonly
        IStockTransactionRepository
        _transactionRepository;

    public InventoryController(
        IInventoryService
            inventoryService,
        IItemRepository
            itemRepository,
        IStockTransactionRepository
            transactionRepository)
    {
        _inventoryService =
            inventoryService;

        _itemRepository =
            itemRepository;

        _transactionRepository =
            transactionRepository;
    }

    [HttpGet("summary")]
    public async Task<IActionResult>
        GetSummary(
            [FromQuery]
            bool lowStockOnly = false)
    {
        var summaryRows =
            await _transactionRepository
                .GetInventorySummaryAsync();

        var items =
            await _itemRepository
                .GetAllAsync();

        var itemsById =
            items.ToDictionary(
                item => item.Id);

        var summary =
            summaryRows
                .Select(row =>
                {
                    itemsById.TryGetValue(
                        row.Id,
                        out var item);

                    return new InventoryItemDto
                    {
                        Id = row.Id,
                        SKU = row.SKU,
                        Name = row.Name,
                        OnHand = row.CurrentStock,
                        LowStockThreshold = item?.LowStockThreshold ?? 0,
                        UnitCostPrice = row.CostPrice,
                        InventoryCostValue = row.InventoryCostValue
                    };
                })
                .ToList();

        if (lowStockOnly)
        {
            summary =
                summary
                    .Where(
                        x => x.IsLowStock)
                    .ToList();
        }

        return Ok(
            new InventorySummaryDto
            {
                TotalInventoryValue = summary
                    .Sum(x => x.InventoryCostValue),

                Items = summary
            });
    }

    [HttpGet("{itemId:int}/stock")]
    public async Task<IActionResult>
        GetStock(
            int itemId)
    {
        var stock =
            await _inventoryService
                .GetOnHandQuantityAsync(
                    itemId);

        return Ok(
            stock);
    }

    [HttpGet("{itemId:int}/value")]
    public async Task<IActionResult>
        GetValue(
            int itemId)
    {
        var value =
            await _inventoryService
                .GetInventoryValueAsync(
                    itemId);

        return Ok(
            value);
    }

[HttpGet("low-stock")]
public async Task<IActionResult>
    GetLowStock()
{
    var lowStockRows =
        await _transactionRepository
            .GetLowStockItemsAsync();

    var results =
            new List<InventoryItemDto>();

    foreach (var item in lowStockRows)
    {
        var onHand =
            await _inventoryService
                .GetOnHandQuantityAsync(item.Id);

        results.Add(
                new InventoryItemDto
            {
                    Id = item.Id,
                SKU = item.SKU,
                Name = item.Name,
                OnHand = onHand,
                    LowStockThreshold = item.LowStockThreshold
            });
    }

    return Ok(results);
}
}
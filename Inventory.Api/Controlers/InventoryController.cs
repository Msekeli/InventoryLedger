using Inventory.Application.Interfaces;
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
        IStockTransactionRepository
        _transactionRepository;

    public InventoryController(
        IInventoryService
            inventoryService,
        IStockTransactionRepository
            transactionRepository)
    {
        _inventoryService =
            inventoryService;

        _transactionRepository =
            transactionRepository;
    }

    [HttpGet("summary")]
    public async Task<IActionResult>
        GetSummary(
            [FromQuery]
            bool lowStockOnly = false)
    {
        var summary =
            await _transactionRepository
                .GetInventorySummaryAsync();

        if (lowStockOnly)
        {
            var lowStock =
                await _transactionRepository
                    .GetLowStockItemsAsync();

            summary =
                summary
                    .Where(
                        x => lowStock
                            .Any(
                                y => y.Id == x.Id))
                    .ToList();
        }

        return Ok(
            new
            {
                TotalInventoryValue =
                    summary.Sum(
                        x => x.InventoryCostValue),

                Items =
                    summary
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
        new List<Inventory.Api.Models.Inventory.InventoryItemDto>();

    foreach (var item in lowStockRows)
    {
        var onHand =
            await _inventoryService
                .GetOnHandQuantityAsync(item.Id);

        results.Add(
            new Inventory.Api.Models.Inventory.InventoryItemDto
            {
                ItemId = item.Id,
                SKU = item.SKU,
                Name = item.Name,
                OnHand = onHand,
                LowStockThreshold = item.LowStockThreshold
            });
    }

    return Ok(results);
}
}
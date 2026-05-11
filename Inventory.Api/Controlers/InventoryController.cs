using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;
    private readonly IItemRepository _itemRepository;

    public InventoryController(
        IInventoryService inventoryService,
        IItemRepository itemRepository)
    {
        _inventoryService = inventoryService;
        _itemRepository = itemRepository;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary(
        [FromQuery] bool lowStockOnly = false)
    {
        var items = await _itemRepository.GetActiveItemsAsync();

        var results = new List<object>();

        decimal totalValue = 0;

        foreach (var item in items)
        {
            int onHand =
                await _inventoryService.GetOnHandQuantityAsync(item.Id);

            decimal value = item.CostPrice * onHand;

            bool isLow = item.IsLowStock(onHand);

            if (lowStockOnly && !isLow)
                continue;

            results.Add(new
            {
                item.Id,
                item.SKU,
                item.Name,
                item.CostPrice,
                item.SellingPrice,
                OnHand = onHand,
                Value = value,
                item.LowStockThreshold,
                IsLowStock = isLow
            });

            totalValue += value;
        }

        return Ok(new
        {
            TotalInventoryValue = totalValue,
            Items = results
        });
    }

    [HttpGet("{itemId:int}/stock")]
    public async Task<IActionResult> GetStock(int itemId)
    {
        var stock =
            await _inventoryService.GetOnHandQuantityAsync(itemId);

        return Ok(stock);
    }

    [HttpGet("{itemId:int}/value")]
    public async Task<IActionResult> GetValue(int itemId)
    {
        var value =
            await _inventoryService.GetInventoryValueAsync(itemId);

        return Ok(value);
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock()
    {
        var items =
            await _inventoryService.GetLowStockItemsAsync();

        return Ok(items);
    }
}
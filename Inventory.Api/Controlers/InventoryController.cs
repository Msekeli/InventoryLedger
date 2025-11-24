using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;
    private readonly IItemRepository _itemRepository;

    public InventoryController(IInventoryService inventoryService, IItemRepository itemRepository)
    {
        _inventoryService = inventoryService;
        _itemRepository = itemRepository;
    }

    // GET: api/inventory/summary?lowStockOnly=true
    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary([FromQuery] bool lowStockOnly = false)
    {
        var items = await _itemRepository.GetAllAsync();
        var results = new List<object>();
        decimal totalValue = 0;

        foreach (var item in items)
        {
            int onHand = await _inventoryService.GetOnHandQuantityAsync(item.Id);
            decimal value = item.UnitPrice * onHand;

            bool isLow = onHand <= item.LowStockThreshold;

            if (lowStockOnly && !isLow)
                continue;

            results.Add(new
            {
                item.Id,
                item.SKU,
                item.Name,
                item.UnitPrice,
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

    // GET: api/inventory/{itemId}/stock
    [HttpGet("{itemId:int}/stock")]
    public async Task<IActionResult> GetStock(int itemId)
    {
        var stock = await _inventoryService.GetOnHandQuantityAsync(itemId);
        return Ok(stock);
    }

    // GET: api/inventory/{itemId}/value
    [HttpGet("{itemId:int}/value")]
    public async Task<IActionResult> GetValue(int itemId)
    {
        var value = await _inventoryService.GetInventoryValueAsync(itemId);
        return Ok(value);
    }

    // GET: api/inventory/low-stock
    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock()
    {
        var items = await _inventoryService.GetLowStockItemsAsync();
        return Ok(items);
    }
}

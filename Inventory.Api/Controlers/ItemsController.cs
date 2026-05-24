using Inventory.Api.Models.Items;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemRepository _itemRepository;
    private readonly IInventoryService _inventoryService;

    public ItemsController(
        IItemRepository itemRepository,
        IInventoryService inventoryService)
    {
        _itemRepository = itemRepository;
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items =
            await _itemRepository.GetActiveItemsAsync();

        var results =
            new List<ItemResponseDto>();

        foreach (var item in items)
        {
            var onHand =
                await _inventoryService
                    .GetOnHandQuantityAsync(
                        item.Id);

            results.Add(
                new ItemResponseDto
                {
                    Id = item.Id,
                    SKU = item.SKU,
                    Name = item.Name,
                    CostPrice = item.CostPrice,
                    SellingPrice = item.SellingPrice,
                    LowStockThreshold =
                        item.LowStockThreshold,
                    SupplierId =
                        item.SupplierId,
                    OnHand = onHand
                });
        }

        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(
        int id)
    {
        var item =
            await _itemRepository.GetByIdAsync(id);

        if (item is null)
            return NotFound();

        var onHand =
            await _inventoryService
                .GetOnHandQuantityAsync(id);

        return Ok(
            new ItemResponseDto
            {
                Id = item.Id,
                SKU = item.SKU,
                Name = item.Name,
                CostPrice = item.CostPrice,
                SellingPrice = item.SellingPrice,
                LowStockThreshold =
                    item.LowStockThreshold,
                SupplierId =
                    item.SupplierId,
                OnHand = onHand
            });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        ItemCreateDto dto)
    {
        var existing =
            await _itemRepository
                .GetBySKUAsync(dto.SKU);

        if (existing is not null)
        {
            return Conflict(
                new
                {
                    message =
                        "SKU already exists."
                });
        }

        var item = new Item
        {
            SKU = dto.SKU,
            Name = dto.Name,
            CostPrice = dto.CostPrice,
            SellingPrice = dto.SellingPrice,
            LowStockThreshold =
                dto.LowStockThreshold,

            // THIS was missing
            SupplierId =
                dto.SupplierId
        };

        await _itemRepository
            .AddAsync(item);

        await _itemRepository
            .SaveChangesAsync();

        return CreatedAtAction(
            nameof(Get),
            new { id = item.Id },
            item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        ItemUpdateDto dto)
    {
        var item =
            await _itemRepository
                .GetByIdAsync(id);

        if (item is null)
            return NotFound();

        item.Name =
            dto.Name;

        item.CostPrice =
            dto.CostPrice;

        item.SellingPrice =
            dto.SellingPrice;

        item.LowStockThreshold =
            dto.LowStockThreshold;

        await _itemRepository
            .UpdateAsync(item);

        await _itemRepository
            .SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        var item =
            await _itemRepository
                .GetByIdAsync(id);

        if (item is null)
            return NotFound();

        item.IsActive = false;

        await _itemRepository
            .UpdateAsync(item);

        await _itemRepository
            .SaveChangesAsync();

        return NoContent();
    }
}
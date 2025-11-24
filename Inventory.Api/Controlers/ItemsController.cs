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

    public ItemsController(IItemRepository itemRepository, IInventoryService inventoryService)
    {
        _itemRepository = itemRepository;
        _inventoryService = inventoryService;
    }

    // GET: api/items
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _itemRepository.GetAllAsync();

        var results = new List<ItemResponseDto>();

        foreach (var item in items)
        {
            var onHand = await _inventoryService.GetOnHandQuantityAsync(item.Id);

            results.Add(new ItemResponseDto
            {
                Id = item.Id,
                SKU = item.SKU,
                Name = item.Name,
                UnitPrice = item.UnitPrice,
                LowStockThreshold = item.LowStockThreshold,
                OnHand = onHand
            });
        }

        return Ok(results);
    }

    // GET: api/items/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item == null) return NotFound();

        var onHand = await _inventoryService.GetOnHandQuantityAsync(id);

        return Ok(new ItemResponseDto
        {
            Id = item.Id,
            SKU = item.SKU,
            Name = item.Name,
            UnitPrice = item.UnitPrice,
            LowStockThreshold = item.LowStockThreshold,
            OnHand = onHand
        });
    }

    // POST: api/items
    [HttpPost]
    public async Task<IActionResult> Create(ItemCreateDto dto)
    {
        var existing = await _itemRepository.GetBySKUAsync(dto.SKU);
        if (existing != null)
            return Conflict(new { message = "SKU already exists." });

        var item = new Item
        {
            SKU = dto.SKU,
            Name = dto.Name,
            UnitPrice = dto.UnitPrice,
            LowStockThreshold = dto.LowStockThreshold
        };

        await _itemRepository.AddAsync(item);
        await _itemRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    // PUT: api/items/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ItemUpdateDto dto)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item == null) return NotFound();

        item.Name = dto.Name;
        item.UnitPrice = dto.UnitPrice;
        item.LowStockThreshold = dto.LowStockThreshold;

        await _itemRepository.UpdateAsync(item);
        await _itemRepository.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/items/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item == null) return NotFound();

        await _itemRepository.DeleteAsync(item);
        await _itemRepository.SaveChangesAsync();

        return NoContent();
    }
}

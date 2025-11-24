using Inventory.Api.Models.Transactions;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockTransactionsController : ControllerBase
{
    private readonly IStockTransactionRepository _transactionRepository;
    private readonly IItemRepository _itemRepository;

    public StockTransactionsController(
        IStockTransactionRepository transactionRepository,
        IItemRepository itemRepository)
    {
        _transactionRepository = transactionRepository;
        _itemRepository = itemRepository;
    }

    // GET: api/stocktransactions/1
    [HttpGet("{itemId:int}")]
    public async Task<IActionResult> GetByItem(int itemId)
    {
        var transactions = await _transactionRepository.GetByItemIdAsync(itemId);
        return Ok(transactions);
    }

    // POST: api/stocktransactions
    [HttpPost]
    public async Task<IActionResult> Create(StockTransactionCreateDto dto)
    {
        // 1. Basic validation
        if (dto.Quantity <= 0)
            return BadRequest(new { message = "Quantity must be > 0" });

        // 2. Ensure Item exists
        var item = await _itemRepository.GetByIdAsync(dto.ItemId);
        if (item == null)
            return NotFound(new { message = "Item not found." });

        // 3. Validate IN/OUT type and calculate QuantityChange
        int quantityChange = dto.Type?.ToUpper() switch
        {
            "IN" => dto.Quantity,
            "OUT" => -dto.Quantity,
            _ => 0
        };

        if (quantityChange == 0)
            return BadRequest(new { message = "Invalid type. Use IN or OUT." });

        // 4. Create transaction
        var transaction = new StockTransaction
        {
            ItemId = dto.ItemId,
            QuantityChange = quantityChange,
            Reference = dto.Remarks,
            Timestamp = DateTime.UtcNow
        };

        await _transactionRepository.AddAsync(transaction);
        await _transactionRepository.SaveChangesAsync();

        return Ok(transaction);
    }
}

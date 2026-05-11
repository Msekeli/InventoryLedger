using Inventory.Api.Models.Transactions;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
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

    [HttpGet("{itemId:int}")]
    public async Task<IActionResult> GetByItem(int itemId)
    {
        var transactions =
            await _transactionRepository.GetByItemIdAsync(itemId);

        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        StockTransactionCreateDto dto)
    {
        if (dto.Quantity <= 0)
        {
            return BadRequest(new
            {
                message = "Quantity must be greater than zero."
            });
        }

        var item =
            await _itemRepository.GetByIdAsync(dto.ItemId);

        if (item is null)
        {
            return NotFound(new
            {
                message = "Item not found."
            });
        }

        var transaction = new StockTransaction
        {
            ItemId = dto.ItemId,
            Quantity = dto.Quantity,
            TransactionType = dto.TransactionType,
            Timestamp = DateTime.UtcNow
        };

        await _transactionRepository.AddAsync(transaction);
        await _transactionRepository.SaveChangesAsync();

        return Ok(transaction);
    }
}
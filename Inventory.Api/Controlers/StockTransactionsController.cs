using Inventory.Api.Models.Transactions;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockTransactionsController
    : ControllerBase
{
    private readonly
        IStockTransactionRepository
        _transactionRepository;

    private readonly
        IItemRepository
        _itemRepository;

    public StockTransactionsController(
        IStockTransactionRepository
            transactionRepository,
        IItemRepository
            itemRepository)
    {
        _transactionRepository =
            transactionRepository;

        _itemRepository =
            itemRepository;
    }

    [HttpGet("{itemId:int}")]
    public async Task<IActionResult>
        GetByItem(
            int itemId)
    {
        var transactions =
            await _transactionRepository
                .GetByItemIdAsync(
                    itemId);

        return Ok(
            transactions);
    }

    [HttpGet("recent")]
    public async Task<IActionResult>
        GetRecent(
            [FromQuery]
            int take = 10)
    {
        var transactions =
            await _transactionRepository
                .GetAllAsync();

        var recent =
            transactions
                .OrderByDescending(
                    x => x.Timestamp)
                .Take(take)
                .ToList();

        return Ok(
            recent);
    }

    [HttpPost("receive-stock")]
    public async Task<IActionResult>
        ReceiveStock(
            StockTransactionCreateDto dto)
    {
        if (
            dto.Quantity <= 0)
        {
            return BadRequest(
                new
                {
                    message =
                        "Quantity must be greater than zero."
                });
        }

        var item =
            await _itemRepository
                .GetByIdAsync(
                    dto.ItemId);

        if (
            item is null)
        {
            return NotFound(
                new
                {
                    message =
                        "Item not found."
                });
        }

        await _transactionRepository
            .ReceiveStockAsync(
                dto.ItemId,
                dto.Quantity,
                "API-STOCK",
                "Stock received from API",
                1);

        return Ok(
            new
            {
                message =
                    "Stock received successfully."
            });
    }
}
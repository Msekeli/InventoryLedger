using Inventory.Domain.Enums;

namespace Inventory.Api.Models.Transactions;

public class StockTransactionCreateDto
{
    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public TransactionType TransactionType { get; set; }

    public string Remarks { get; set; } = string.Empty;
}
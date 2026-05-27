namespace Inventory.Client.Models.Transactions;

public class CreateStockTransactionDto
{
    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public int TransactionType { get; set; }

    public string ReferenceNumber { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;
}
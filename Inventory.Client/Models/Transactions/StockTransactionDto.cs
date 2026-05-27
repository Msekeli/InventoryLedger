namespace Inventory.Client.Models.Transactions;

public class StockTransactionDto
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public int TransactionType { get; set; }

    public string ReferenceNumber { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public bool IsIncrease =>
        TransactionType == 1;

    public bool IsDecrease =>
        TransactionType == 2;
}
namespace Inventory.Client.Models.Transactions;

public class StockTransactionDto
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int QuantityChange { get; set; }
    public string Reference { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public bool IsIncrease => QuantityChange > 0;
    public bool IsDecrease => QuantityChange < 0;
}

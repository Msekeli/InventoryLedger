namespace Inventory.Api.Models.Transactions;

public class StockTransactionCreateDto
{
    public int ItemId { get; set; }

    // Quantity can be positive (IN) or negative (OUT)
    public int Quantity { get; set; }

    // "IN" or "OUT"
    public string Type { get; set; } = "IN";

    // Optional comment
    public string Remarks { get; set; } = string.Empty;
}

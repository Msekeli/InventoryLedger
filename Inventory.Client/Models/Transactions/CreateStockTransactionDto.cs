namespace Inventory.Client.Models.Transactions;

public class CreateStockTransactionDto
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public string Type { get; set; } = "IN"; // IN / OUT
    public string Remarks { get; set; } = string.Empty;
}

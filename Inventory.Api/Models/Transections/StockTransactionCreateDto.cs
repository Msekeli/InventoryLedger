namespace Inventory.Api.Models.Transactions;

public class StockTransactionCreateDto
{
    public int ItemId { get; set; }

    // Swagger sends "quantity"
    public int Quantity { get; set; }

    // Swagger sends "type": "IN" or "OUT"
    public string Type { get; set; } = "IN";

    // Swagger sends "remarks"
    public string Remarks { get; set; } = string.Empty;
}

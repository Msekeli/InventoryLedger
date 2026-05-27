namespace Inventory.Client.Models.Items;

public class ItemUpdateDto
{
    public string Name { get; set; } = string.Empty;

    public decimal CostPrice { get; set; }

    public decimal SellingPrice { get; set; }

    public int LowStockThreshold { get; set; }
}
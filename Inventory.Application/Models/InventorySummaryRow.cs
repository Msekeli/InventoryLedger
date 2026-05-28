namespace Inventory.Application.Models;

public class InventorySummaryRow
{
    public int Id { get; set; }

    public string SKU { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int CurrentStock { get; set; }

    public decimal CostPrice { get; set; }

    public decimal SellingPrice { get; set; }

    public decimal InventoryCostValue { get; set; }

    public decimal InventorySellingValue { get; set; }
}
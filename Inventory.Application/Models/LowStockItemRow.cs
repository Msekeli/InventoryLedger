namespace Inventory.Application.Models;

public class LowStockItemRow
{
    public int Id { get; set; }

    public string SKU { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int CurrentStock { get; set; }

    public int LowStockThreshold { get; set; }
}
namespace Inventory.Api.Models.Inventory;

public class InventorySummaryDto
{
    public decimal TotalInventoryValue { get; set; }

    public List<InventoryItemDto> Items { get; set; } = new();
}

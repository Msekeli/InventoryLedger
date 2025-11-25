namespace Inventory.Api.Models.Inventory;

public class InventorySummaryDto
{
    public List<InventoryItemDto> Items { get; set; } = new();
}

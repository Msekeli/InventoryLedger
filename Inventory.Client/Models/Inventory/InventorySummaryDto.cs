namespace Inventory.Client.Models.Inventory
{
    public class InventorySummaryDto
    {
        public List<InventoryItemDto> Items { get; set; } = new();
    }

    public class InventoryItemDto
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int OnHand { get; set; }
        public int LowStockThreshold { get; set; }

        public bool IsLow => OnHand < LowStockThreshold;
    }
}

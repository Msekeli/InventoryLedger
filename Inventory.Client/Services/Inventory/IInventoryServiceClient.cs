using Inventory.Client.Models.Inventory;

namespace Inventory.Client.Services.Inventory
{
    public interface IInventoryServiceClient
    {
        Task<InventorySummaryDto> GetSummaryAsync();
        Task<List<InventoryItemDto>> GetLowStockItemsAsync();
        Task<InventoryItemDto?> GetItemSummaryAsync(int itemId);
    }
}

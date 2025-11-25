using System.Net.Http.Json;
using Inventory.Client.Models.Inventory;

namespace Inventory.Client.Services.Inventory
{
    public class InventoryServiceClient : IInventoryServiceClient
    {
        private readonly HttpClient _http;

        public InventoryServiceClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<InventorySummaryDto> GetSummaryAsync()
        {
            var result = await _http.GetFromJsonAsync<InventorySummaryDto>("api/inventory/summary");
            return result ?? new InventorySummaryDto();
        }

        public async Task<List<InventoryItemDto>> GetLowStockItemsAsync()
        {
            var result = await _http.GetFromJsonAsync<List<InventoryItemDto>>("api/inventory/low-stock");
            return result ?? new List<InventoryItemDto>();
        }

        public async Task<InventoryItemDto?> GetItemSummaryAsync(int itemId)
        {
            return await _http.GetFromJsonAsync<InventoryItemDto>($"api/inventory/{itemId}");
        }
    }
}

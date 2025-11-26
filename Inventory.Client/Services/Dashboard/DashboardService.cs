using System.Net.Http.Json;
using Inventory.Client.Models.Items;
using Inventory.Client.Models.Inventory;
using Inventory.Client.Models.Transactions;

namespace Inventory.Client.Services.Dashboard;

public class DashboardService
{
    private readonly HttpClient _http;

    public DashboardService(HttpClient http)
    {
        _http = http;
    }

    // Get all items (id, name, sku, price, onHand etc.)
    public async Task<List<ItemResponseDto>> GetAllItemsAsync()
        => await _http.GetFromJsonAsync<List<ItemResponseDto>>("api/items") ?? new();

    // Get full inventory summary (value + list)
    public async Task<InventorySummaryDto?> GetSummaryAsync()
        => await _http.GetFromJsonAsync<InventorySummaryDto>("api/inventory/summary");

    // Get low stock list
    public async Task<List<InventoryItemDto>> GetLowStockAsync()
    {
        var summary =
            await _http.GetFromJsonAsync<InventorySummaryDto>("api/inventory/summary?lowStockOnly=true");

        return summary?.Items ?? new();
    }

    // Get recent transactions (we merge all items' transactions)
    public async Task<List<StockTransactionDto>> GetRecentTransactionsAsync(int take = 8)
    {
        var items = await GetAllItemsAsync();
        var all = new List<StockTransactionDto>();

        foreach (var item in items)
        {
            var tx = await _http.GetFromJsonAsync<List<StockTransactionDto>>(
                $"api/stocktransactions/{item.Id}"
            );

            if (tx is not null)
            {
                // Attach the item name to reference (client-side enrichment)
                foreach (var t in tx)
                {
                    // Safe fallback if backend reference is empty
                    t.Reference = $"{item.Name} | {t.Reference}".TrimEnd('|', ' ');
                }

                all.AddRange(tx);
            }
        }

        return all
            .OrderByDescending(t => t.Timestamp)
            .Take(take)
            .ToList();
    }
}

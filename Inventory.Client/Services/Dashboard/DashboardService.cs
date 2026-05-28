using System.Net.Http.Json;
using Inventory.Client.Models.Inventory;
using Inventory.Client.Models.Items;
using Inventory.Client.Models.Transactions;

namespace Inventory.Client.Services.Dashboard;

public class DashboardService
{
    private readonly HttpClient _http;

    public DashboardService(
        HttpClient http)
    {
        _http = http;
    }

    // ITEMS

    public async Task<List<ItemResponseDto>>
        GetAllItemsAsync()
    {
        return await _http
            .GetFromJsonAsync<List<ItemResponseDto>>(
                "api/items")
            ?? new();
    }

    // INVENTORY SUMMARY

    public async Task<InventorySummaryDto?>
        GetSummaryAsync()
    {
        return await _http
            .GetFromJsonAsync<InventorySummaryDto>(
                "api/inventory/summary");
    }

    // LOW STOCK

    public async Task<List<InventoryItemDto>>
        GetLowStockAsync()
    {
        return await _http
            .GetFromJsonAsync<List<InventoryItemDto>>(
                "api/inventory/low-stock")
            ?? new();
    }

    // RECENT TRANSACTIONS

    public async Task<List<StockTransactionDto>>
        GetRecentTransactionsAsync(
            int take = 10)
    {
        return await _http
            .GetFromJsonAsync<List<StockTransactionDto>>(
                $"api/stocktransactions/recent?take={take}")
            ?? new();
    }
}
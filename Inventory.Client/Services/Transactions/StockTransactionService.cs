using System.Net.Http.Json;
using Inventory.Client.Models.Transactions;

namespace Inventory.Client.Services.Transactions;

public class StockTransactionService : IStockTransactionService
{
    private readonly HttpClient _http;

    public StockTransactionService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<StockTransactionDto>> GetByItemAsync(int itemId)
    {
        return await _http.GetFromJsonAsync<List<StockTransactionDto>>(
            $"api/stocktransactions/{itemId}"
        ) ?? new List<StockTransactionDto>();
    }

    public async Task<bool> CreateAsync(CreateStockTransactionDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/stocktransactions", dto);
        return response.IsSuccessStatusCode;
    }
}

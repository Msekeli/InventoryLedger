using System.Net.Http.Json;
using Inventory.Client.Models.Transactions;

namespace Inventory.Client.Services.Transactions
{
    public class StockTransactionService : IStockTransactionService
    {
        private readonly HttpClient _http;

        public StockTransactionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<StockTransactionDto>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<StockTransactionDto>>("api/StockTransactions/recent");
            return result ?? new List<StockTransactionDto>();
        }

        public async Task<List<StockTransactionDto>> GetByItemIdAsync(int itemId)
        {
            var result = await _http.GetFromJsonAsync<List<StockTransactionDto>>($"api/StockTransactions/{itemId}");
            return result ?? new List<StockTransactionDto>();
        }

        public async Task<bool> CreateAsync(CreateStockTransactionDto model)
        {
            var response = await _http.PostAsJsonAsync("api/StockTransactions/receive-stock", model);
            return response.IsSuccessStatusCode;
        }
    }
}

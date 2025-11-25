namespace Inventory.Client.Services.Transactions;

public class StockTransactionService : IStockTransactionService
{
    private readonly HttpClient _http;

    public StockTransactionService(HttpClient http)
    {
        _http = http;
    }
}

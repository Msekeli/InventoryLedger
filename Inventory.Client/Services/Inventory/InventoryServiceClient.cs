namespace Inventory.Client.Services.Inventory;

public class InventoryServiceClient : IInventoryServiceClient
{
    private readonly HttpClient _http;

    public InventoryServiceClient(HttpClient http)
    {
        _http = http;
    }
}

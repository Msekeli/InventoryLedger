using System.Net.Http.Json;
using Inventory.Client.Models.Suppliers;

namespace Inventory.Client.Services.Suppliers;

public class SupplierService
    : ISupplierService
{
    private readonly HttpClient _httpClient;

    public SupplierService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SupplierDto>>
        GetAllAsync()
    {
        var suppliers =
            await _httpClient
                .GetFromJsonAsync<List<SupplierDto>>(
                    "api/Suppliers");

        return suppliers ?? new();
    }

    public async Task<bool> CreateAsync(SupplierCreateDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Suppliers", dto);
        return response.IsSuccessStatusCode;
    }
}
using System.Net.Http.Json;
using Inventory.Client.Models.Items;

namespace Inventory.Client.Services.Items;

public class ItemService : IItemService
{
    private readonly HttpClient _http;

    public ItemService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ItemResponseDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<ItemResponseDto>>("api/items")
               ?? new List<ItemResponseDto>();
    }

    public async Task<ItemResponseDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<ItemResponseDto>($"api/items/{id}");
    }

    public async Task<bool> CreateAsync(ItemCreateDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/items", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(int id, ItemUpdateDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/items/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/items/{id}");
        return response.IsSuccessStatusCode;
    }
}

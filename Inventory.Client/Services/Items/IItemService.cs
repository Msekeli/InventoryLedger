using Inventory.Client.Models.Items;

namespace Inventory.Client.Services.Items;

public interface IItemService
{
    Task<List<ItemResponseDto>> GetAllAsync();
    Task<ItemResponseDto?> GetByIdAsync(int id);
    Task<bool> CreateAsync(ItemCreateDto dto);
    Task<bool> UpdateAsync(int id, ItemUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

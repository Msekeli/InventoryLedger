using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly InventoryDbContext _context;

    public ItemRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _context.Items.FindAsync(id);
    }

    public async Task<Item?> GetBySKUAsync(string sku)
    {
        return await _context.Items.FirstOrDefaultAsync(i => i.SKU == sku);
    }

    public async Task<List<Item>> GetAllAsync()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task AddAsync(Item item)
    {
        await _context.Items.AddAsync(item);
    }

    public Task UpdateAsync(Item item)
    {
        _context.Items.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Item item)
    {
        _context.Items.Remove(item);
        return Task.CompletedTask;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}

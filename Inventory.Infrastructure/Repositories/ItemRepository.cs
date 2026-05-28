using Inventory.Application.Interfaces;
using Inventory.Domain.Enums;
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
        return await _context.Items
            .FirstOrDefaultAsync(i => i.SKU == sku);
    }

    public async Task<List<Item>> GetAllAsync()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<List<Item>> GetActiveItemsAsync()
    {
        return await _context.Items
            .Where(i => i.IsActive)
            .ToListAsync();
    }

    public async Task<List<Item>> GetLowStockItemsAsync()
    {
        var items = await _context.Items
            .Where(i => i.IsActive)
            .ToListAsync();

        var transactions = await _context.StockTransactions
            .Select(t => new
            {
                t.ItemId,
                t.Quantity,
                t.TransactionType
            })
            .ToListAsync();

        var currentStockByItem = transactions
            .GroupBy(t => t.ItemId)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(t =>
                    t.TransactionType == TransactionType.StockReceived ? t.Quantity :
                    t.TransactionType == TransactionType.Sale ? -t.Quantity :
                    t.TransactionType == TransactionType.CustomerReturn ? t.Quantity :
                    t.TransactionType == TransactionType.SupplierReturn ? -t.Quantity :
                    t.TransactionType == TransactionType.Damaged ? -t.Quantity :
                    t.TransactionType == TransactionType.Expired ? -t.Quantity :
                    t.TransactionType == TransactionType.Adjustment ? t.Quantity :
                    t.TransactionType == TransactionType.StockCountCorrection ? t.Quantity :
                    0));

        return items
            .Where(i =>
                currentStockByItem.TryGetValue(i.Id, out var onHand)
                && onHand < i.LowStockThreshold)
            .ToList();
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
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class StockTransactionRepository : IStockTransactionRepository
{
    private readonly InventoryDbContext _context;

    public StockTransactionRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<List<StockTransaction>> GetByItemIdAsync(int itemId)
    {
        return await _context.StockTransactions
            .Where(t => t.ItemId == itemId)
            .ToListAsync();
    }

    public async Task AddAsync(StockTransaction transaction)
    {
        await _context.StockTransactions.AddAsync(transaction);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}

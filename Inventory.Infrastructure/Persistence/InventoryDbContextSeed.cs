using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence;

public static class InventoryDbContextSeed
{
    public static async Task SeedAsync(InventoryDbContext db)
    {
        if (await db.Items.AnyAsync()) return;

        var items = new List<Item>
        {
            new Item { SKU = "APL001", Name = "Apple", UnitPrice = 5.00m, LowStockThreshold = 20 },
            new Item { SKU = "BNN001", Name = "Banana", UnitPrice = 3.00m, LowStockThreshold = 15 },
            new Item { SKU = "ORG001", Name = "Orange", UnitPrice = 4.50m, LowStockThreshold = 10 }
        };

        await db.Items.AddRangeAsync(items);
        await db.SaveChangesAsync();

        var transactions = new List<StockTransaction>
        {
            new StockTransaction { ItemId = items[0].Id, QuantityChange = 100, Reference = "Initial stock" },
            new StockTransaction { ItemId = items[1].Id, QuantityChange = 50, Reference = "Initial stock" },
            new StockTransaction { ItemId = items[2].Id, QuantityChange = 80, Reference = "Initial stock" },

            new StockTransaction { ItemId = items[0].Id, QuantityChange = -10, Reference = "Sold" },
            new StockTransaction { ItemId = items[1].Id, QuantityChange = -5, Reference = "Sold" }
        };

        await db.StockTransactions.AddRangeAsync(transactions);
        await db.SaveChangesAsync();
    }
}

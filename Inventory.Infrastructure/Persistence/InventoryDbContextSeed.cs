using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence;

public static class InventoryDbContextSeed
{
    public static async Task SeedAsync(InventoryDbContext db)
    {
        // Force wipe existing items & transactions
        db.StockTransactions.RemoveRange(db.StockTransactions);
        db.Items.RemoveRange(db.Items);
        await db.SaveChangesAsync();

        // Seed Items
        var items = new List<Item>
        {
            new Item { SKU = "SP-001", Name = "Steak & Chops Spice", UnitPrice = 29.99m, LowStockThreshold = 20 },
            new Item { SKU = "SP-002", Name = "Chicken BBQ Spice", UnitPrice = 25.50m, LowStockThreshold = 15 },
            new Item { SKU = "SP-003", Name = "Curry Spice Medium", UnitPrice = 23.75m, LowStockThreshold = 15 },
            new Item { SKU = "SP-004", Name = "Garlic & Herb Spice", UnitPrice = 21.90m, LowStockThreshold = 10 },
            new Item { SKU = "SA-001", Name = "Beef Sausage (1kg)", UnitPrice = 89.99m, LowStockThreshold = 10 },
            new Item { SKU = "SA-002", Name = "Pork Sausage (1kg)", UnitPrice = 92.50m, LowStockThreshold = 10 },
            new Item { SKU = "BG-001", Name = "Beef Burger Patty (4pcs)", UnitPrice = 69.99m, LowStockThreshold = 12 },
            new Item { SKU = "BG-002", Name = "Chicken Burger Patty (4pcs)", UnitPrice = 62.50m, LowStockThreshold = 12 },
            new Item { SKU = "MX-001", Name = "Boerewors Mix Spice", UnitPrice = 34.99m, LowStockThreshold = 15 },
            new Item { SKU = "MX-002", Name = "Sausage Casing (10m)", UnitPrice = 110.00m, LowStockThreshold = 5 }
        };

        await db.Items.AddRangeAsync(items);
        await db.SaveChangesAsync();

        // Seed Initial Stock (100 in for each item)
        var transactions = items.Select(i => new StockTransaction
        {
            ItemId = i.Id,
            QuantityChange = 100,
            Reference = "Initial stock"
        });

        await db.StockTransactions.AddRangeAsync(transactions);
        await db.SaveChangesAsync();
    }
}

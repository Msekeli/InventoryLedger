using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence;

public static class InventoryDbContextSeed
{
    public static async Task SeedAsync(InventoryDbContext db)
    {
        // Wipe data
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

        var random = new Random();
        var transactions = new List<StockTransaction>();

        // Generate 30 days of realistic activity
        foreach (var item in items)
        {
            int startingStock = random.Next(80, 150); // Each item gets a random realistic starting amount

            // 1. Add initial stock
            transactions.Add(new StockTransaction
            {
                ItemId = item.Id,
                QuantityChange = startingStock,
                Reference = "Initial stock",
                Timestamp = DateTime.UtcNow.AddDays(-30).AddHours(random.Next(1, 8))
            });

            // 2. Now simulate activity for each of the last 30 days
            for (int day = 0; day < 30; day++)
            {
                DateTime dayTimestamp = DateTime.UtcNow.AddDays(-day);

                // Chance of a sale each day
                if (random.NextDouble() < 0.7)
                {
                    int saleQty = random.Next(1, 8);
                    transactions.Add(new StockTransaction
                    {
                        ItemId = item.Id,
                        QuantityChange = -saleQty,
                        Reference = "Sale",
                        Timestamp = dayTimestamp.AddHours(random.Next(8, 18))
                    });
                }

                // Chance of restock every few days
                if (random.NextDouble() < 0.2)
                {
                    int restockQty = random.Next(10, 40);
                    transactions.Add(new StockTransaction
                    {
                        ItemId = item.Id,
                        QuantityChange = restockQty,
                        Reference = "Restock",
                        Timestamp = dayTimestamp.AddHours(random.Next(10, 16))
                    });
                }
            }
        }

        await db.StockTransactions.AddRangeAsync(transactions);
        await db.SaveChangesAsync();
    }
}

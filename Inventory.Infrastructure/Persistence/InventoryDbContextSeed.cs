using Bogus;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Infrastructure.Persistence;

public static class InventoryDbContextSeed
{
    public static async Task SeedAsync(InventoryDbContext db)
    {
        Randomizer.Seed = new Random(2026);

        // Remove generated operational data only
        db.StockTransactions.RemoveRange(db.StockTransactions);
        db.AppUsers.RemoveRange(db.AppUsers);
        db.AppRoles.RemoveRange(db.AppRoles);

        await db.SaveChangesAsync();

        // =========================
        // Roles
        // =========================

        var roles = new List<AppRole>
        {
            new() { Name = "Owner" },
            new() { Name = "Cashier" },
            new() { Name = "Stock Clerk" }
        };

        await db.AppRoles.AddRangeAsync(roles);
        await db.SaveChangesAsync();

        // =========================
        // Users
        // =========================

        var users = new Faker<AppUser>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.IsActive, true)
            .RuleFor(u => u.AppRoleId, f => f.PickRandom(roles).Id)
            .Generate(5);

        await db.AppUsers.AddRangeAsync(users);
        await db.SaveChangesAsync();

        // =========================
        // IMPORTANT:
        // Items and Suppliers are
        // manually curated business data.
        // Seeder must NOT recreate them.
        // =========================

        var items = db.Items.ToList();

        if (!items.Any())
            return;

        var random = new Random(2026);

        var transactions = new List<StockTransaction>();

        foreach (var item in items)
        {
            int currentStock = random.Next(30, 80);

            // =========================
            // Opening Stock
            // =========================

            transactions.Add(new StockTransaction
            {
                ItemId = item.Id,
                Quantity = currentStock,
                TransactionType = TransactionType.StockReceived,
                Timestamp = DateTime.UtcNow.AddDays(-30),
                ReferenceNumber = $"OPEN-{item.Id}",
                Notes = "Opening stock balance",
                PerformedByUserId = random.Next(1, 6)
            });

            int movementCount = random.Next(6, 12);

            DateTime movementDate = DateTime.UtcNow.AddDays(-29);

            for (int i = 0; i < movementCount; i++)
            {
                movementDate = movementDate.AddDays(random.Next(1, 4));

                bool restock = random.NextDouble() < 0.25;

                // =========================
                // STOCK RECEIVED
                // =========================

                if (restock)
                {
                    int receivedQty = random.Next(10, 35);

                    currentStock += receivedQty;

                    transactions.Add(new StockTransaction
                    {
                        ItemId = item.Id,
                        Quantity = receivedQty,
                        TransactionType = TransactionType.StockReceived,
                        Timestamp = movementDate,
                        ReferenceNumber = $"REC-{item.Id}-{i}",
                        Notes = "Supplier stock received",
                        PerformedByUserId = random.Next(1, 6)
                    });

                    continue;
                }

                // =========================
                // Prevent Negative Stock
                // =========================

                if (currentStock <= 5)
                    continue;

                // =========================
                // SALE
                // =========================

                int saleQty = random.Next(1, Math.Min(currentStock, 8));

                currentStock -= saleQty;

                transactions.Add(new StockTransaction
                {
                    ItemId = item.Id,
                    Quantity = saleQty,
                    TransactionType = TransactionType.Sale,
                    Timestamp = movementDate,
                    ReferenceNumber = $"SAL-{item.Id}-{i}",
                    Notes = "Customer sale",
                    PerformedByUserId = random.Next(1, 6)
                });
            }
        }

        await db.StockTransactions.AddRangeAsync(transactions);

        await db.SaveChangesAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Entities;

namespace Inventory.Infrastructure.Persistence;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(
        DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Item> Items => Set<Item>();

    public DbSet<StockTransaction> StockTransactions
        => Set<StockTransaction>();

    public DbSet<Supplier> Suppliers
        => Set<Supplier>();

    public DbSet<AppRole> AppRoles
        => Set<AppRole>();

    public DbSet<AppUser> AppUsers
        => Set<AppUser>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Existing Azure table names
        modelBuilder.Entity<Item>()
            .ToTable("Items");

        modelBuilder.Entity<StockTransaction>()
            .ToTable("StockTransactions");

        modelBuilder.Entity<Supplier>()
            .ToTable("Supplier");

        modelBuilder.Entity<AppRole>()
            .ToTable("AppRole");

        modelBuilder.Entity<AppUser>()
            .ToTable("AppUser");

        // Relationships
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Supplier)
            .WithMany()
            .HasForeignKey(i => i.SupplierId);

        modelBuilder.Entity<StockTransaction>()
            .HasOne(t => t.Item)
            .WithMany()
            .HasForeignKey(t => t.ItemId);

        modelBuilder.Entity<StockTransaction>()
            .HasOne(t => t.PerformedBy)
            .WithMany()
            .HasForeignKey(
                t => t.PerformedByUserId);

        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(
                u => u.AppRoleId);

        // Money precision
        modelBuilder.Entity<Item>()
            .Property(i => i.CostPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Item>()
            .Property(i => i.SellingPrice)
            .HasPrecision(18, 2);
    }
}
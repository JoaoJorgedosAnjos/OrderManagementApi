using Microsoft.EntityFrameworkCore;
using OrderManagementApi.Models.Entities;

namespace OrderManagementApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .HasKey(i => new { i.OrderId, i.ProductId });

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId);

        modelBuilder.Entity<Order>()
            .Property(o => o.Value)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Item>()
            .Property(i => i.Price)
            .HasColumnType("decimal(18,2)");
    }
}
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entitites;

namespace OrderManagementSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.ProductName)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(o => o.Price)
                  .IsRequired();
            entity.Property(o => o.Status)
                  .IsRequired();
        });
    }
}
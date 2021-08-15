using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder
                .Entity<Customer>()
                .ToTable("Customer")
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Address>()
                .ToTable("Address")
                .HasKey(x => x.CustomerId);

            modelBuilder
                .Entity<Product>()
                .ToTable("Product")
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Order>()
                .ToTable("Order")
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<OrderLine>()
                .ToTable("OrderLine")
                .HasKey(x => new { x.OrderId, x.ProductId });
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}

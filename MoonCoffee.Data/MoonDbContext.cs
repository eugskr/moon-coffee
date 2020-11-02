using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoonCoffee.Data.Models;

namespace MoonCoffee.Data
{
    public class MoonDbContext : IdentityDbContext
    {
        public MoonDbContext()
        {
        }
        public MoonDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAdresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductInventorySnapshot> ProductInventorySnapshots { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
      

    }
}
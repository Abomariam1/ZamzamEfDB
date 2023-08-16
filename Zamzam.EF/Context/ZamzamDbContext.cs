using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamzam.Core;
using Zamzam.Core.Entites;

namespace Zamzam.EF
{
    public class ZamzamDbContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<MaintenanceDetail> MaintenanceOrderLines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderLines { get; set; }
        public DbSet<ReturnPurchaseOrder> ReturnPurchaseOrders { get; set; }
        public DbSet<ReturnPurchaseOrderDetailes> ReturnPurchaseOrderLines { get; set; }
        public DbSet<ReturnSaleOrder> ReturnSaleOrders { get; set; }
        public DbSet<ReturnSaleOrderDetail> ReturnSaleOrderLines { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderDetail> SaleOrderLines { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }

        public ZamzamDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

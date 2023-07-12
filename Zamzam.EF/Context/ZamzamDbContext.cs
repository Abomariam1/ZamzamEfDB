using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamzam.Core;

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
        public DbSet<MaintenanceOrderLine> MaintenanceOrderLines { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }
        public DbSet<ReturnPurchaseOrder> ReturnPurchaseOrders { get; set; }
        public DbSet<ReturnPurchaseOrderLine> ReturnPurchaseOrderLines { get; set; }
        public DbSet<ReturnSaleOrder> ReturnSaleOrders { get; set; }
        public DbSet<ReturnSaleOrderLine> ReturnSaleOrderLines { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderLine> SaleOrderLines { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public ZamzamDbContext(DbContextOptions options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ZamzamDb;Integrated Security=True;TrustServerCertificate=True");
        //    base.OnConfiguring(optionsBuilder);

        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

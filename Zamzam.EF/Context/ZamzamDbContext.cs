using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamzam.Core;

namespace ZamzamEfDb
{
    public class ZamzamDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ZamzamDb;Integrated Security=True;TrustServerCertificate=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //new AreaEntityConfiguration().Configure(modelBuilder.Entity<Area>());
            //new CustomerEntityConfiguration().Configure(modelBuilder.Entity<Customer>());
            //new DepartmentEntityConfiguration().Configure(modelBuilder.Entity<Department>());
            //new EmployeeEntityConfeguration().Configure(modelBuilder.Entity<Employee>());
            //new ItemEntityConfiguration().Configure(modelBuilder.Entity<Item>());
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AreaEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartmentEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeEntityConfeguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleOrderLineConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleOrderEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZamzamDbContext).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

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
    }
}

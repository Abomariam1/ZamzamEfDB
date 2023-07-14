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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MOSTAFA\\SQLEXPRESS;Initial Catalog=ZamzamDb;Integrated Security=True;TrustServerConnection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new AreaEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new ItemEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new DepartmentEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new EmployeeEntityConfeguration());
            //modelBuilder.ApplyConfiguration(new InstallmentEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new MaintenanceOrderLineEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new PurchaseOrderConfiguratin());
            //modelBuilder.ApplyConfiguration(new PurchaseOrderLineConfiguration());
            //modelBuilder.ApplyConfiguration(new ReturnPurchaseOrderConfiguration());
            //modelBuilder.ApplyConfiguration(new ReturnSaleOrderEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new ReturnSaleOrderLineEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new SaleOrderEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new SaleOrderLineConfiguration());
            //modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AreaEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartmentEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeEntityConfeguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(InstallmentEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(MaintenanceOrderLineEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(PurchaseOrderLineConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReturnPurchaseOrderConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReturnSaleOrderEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReturnSaleOrderLineEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleOrderEntityConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleOrderLineConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SupplierConfiguration).Assembly);
            //new AreaEntityConfiguration().Configure(modelBuilder.Entity<Area>());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

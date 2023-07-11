using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamzam.Core;

namespace ZamzamEfDb.Test.Context
{
    public class ZamzamDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=MOSTAFA\\SQLEXPRESS;Initial Catalog=ZamzamDbTest;Integrated Security=True;TrustServerCertificate=True");

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
    }
}

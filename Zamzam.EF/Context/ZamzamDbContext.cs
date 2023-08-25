using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamzam.Domain;
using Zamzam.Domain.Common;
using Zamzam.Domain.Common.Interfaces;
using Zamzam.Domain.Entites;

namespace Zamzam.EF
{
    public class ZamzamDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public DbSet<Area> Areas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<InstallmentedSaleOrder> InstallmentedSaleOrders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<ReturnPurchaseOrder> ReturnPurchaseOrders { get; set; }
        public DbSet<ReturnSaleOrder> ReturnSaleOrders { get; set; }
        public DbSet<InstallmentedSaleOrder> SaleOrders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }

        //public ZamzamDbContext(DbContextOptions options) : base(options) { }

        public ZamzamDbContext(DbContextOptions<ZamzamDbContext> options, IDomainEventDispatcher dispatcher)
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            if (_dispatcher == null)
                return result;
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();
            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
            return result;
        }
        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}

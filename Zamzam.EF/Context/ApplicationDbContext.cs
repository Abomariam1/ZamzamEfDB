using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamzam.Domain;
using Zamzam.Domain.Common;
using Zamzam.Domain.Common.Interfaces;
using Zamzam.Domain.Entites;

namespace Zamzam.EF
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDomainEventDispatcher dispatcher)
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public DbSet<Area> Areas => Set<Area>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Employee> Employees => Set<Employee>();
        //public DbSet<Installment> Installments { get; set; }
        //public DbSet<InstallmentedSaleOrder> InstallmentedSaleOrders { get; set; }
        public DbSet<Item> Items => Set<Item>();
        //public DbSet<Maintenance> Maintenances { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        //public DbSet<ReturnPurchaseOrder> ReturnPurchaseOrders { get; set; }
        //public DbSet<ReturnSaleOrder> ReturnSaleOrders { get; set; }
        //public DbSet<InstallmentedSaleOrder> SaleOrders { get; set; }
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<User> Users => Set<User>();

        //public ZamzamDbContext(DbContextOptions options) : base(options) { }


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

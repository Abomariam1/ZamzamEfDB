using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamzam.Application.Security;
using Zamzam.Domain;
using Zamzam.Domain.Common;
using Zamzam.Domain.Common.Interfaces;
using Zamzam.Domain.Entites;

namespace Zamzam.EF;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
{
    private readonly IDomainEventDispatcher _dispatcher;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IDomainEventDispatcher dispatcher)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Installment> Installments => Set<Installment>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    public DbSet<SaleOrder> SaleOrders => Set<SaleOrder>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<ReturnSaleOrder> ReturnSaleOrders => Set<ReturnSaleOrder>();
    public DbSet<ReturnPurchaseOrder> ReturnPurchaseOrders => Set<ReturnPurchaseOrder>();
    public DbSet<InstallmentedSaleOrder> InstallmentSaleOrders => Set<InstallmentedSaleOrder>();
    public DbSet<Maintenance> Maintenances => Set<Maintenance>();




    //public ZamzamDbContext(DbContextOptions options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if(_dispatcher == null)
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

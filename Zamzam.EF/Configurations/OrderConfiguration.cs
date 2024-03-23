using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain;
using Zamzam.Domain.Types;

namespace Zamzam.EF.Configurations;

public class OrderConfiguration: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(x => x.Id);
        builder.UseTptMappingStrategy();
        builder.Property(x => x.InvoiceType)
            .HasConversion(
            x => x.ToString(),
            x => (InvoiceType)Enum.Parse(typeof(InvoiceType), x))
            .HasMaxLength(20);

        builder.Property(x => x.OrderType)
            .HasConversion(
            x => x.ToString(),
            x => (OrderType)Enum.Parse(typeof(OrderType), x))
            .HasMaxLength(20);

        builder.HasOne(x => x.Employee)
             .WithMany(c => c.Orders)
             .HasForeignKey(x => x.EmployeeId)
             .OnDelete(DeleteBehavior.Restrict);


        builder.Property(d => d.OrderDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(t => t.TotalPrice)
            .HasPrecision(9, 2);

        builder.Property(t => t.TotalDiscount)
            .HasPrecision(9, 2);

        builder.Property(t => t.TotalPayed)
            .HasPrecision(9, 2);
        builder.Property(t => t.TotalRemaining)
            .HasPrecision(9, 2);

        builder.Property(s => s.NetPrice)
            .HasPrecision(9, 2)
            .HasComputedColumnSql("[TotalPrice] - [TotalDiscount]");

    }
}

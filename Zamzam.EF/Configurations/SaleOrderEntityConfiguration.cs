using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class SaleOrderEntityConfiguration : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {

            //builder.Property(x => x.Id)
            //    .HasConversion(UlidToGuidValueConverter.ulidconverter)
            //    .HasColumnType("uniqueidentifier");

            builder.HasKey(x => x.Id);

            builder.Property(d => d.OrderDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.TotalPrice)
                .HasPrecision(9, 2);

            builder.Property(r => r.Remains)
                .HasPrecision(9, 2);

            builder.Property(s => s.TotalDiscount)
                .HasPrecision(9, 2);

            builder.Property(s => s.Payed)
                .HasPrecision(9, 2);

            builder.Property(s => s.NetPrice)
                .HasPrecision(9, 2)
                .HasComputedColumnSql("[TotalPrice] - [TotalDiscount]");

            builder.HasOne(c => c.Customer)
                .WithMany(c => c.SaleOrders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Employee)
                .WithMany(c => c.Sales)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

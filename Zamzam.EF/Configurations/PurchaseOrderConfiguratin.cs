using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class PurchaseOrderConfiguratin : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
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

            builder.HasOne(s => s.Supplier)
                .WithMany(s => s.PurchaseOrders)
                .HasForeignKey(s => s.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Employee)
                .WithMany(p => p.Purchases)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

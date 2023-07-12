using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace ZamzamEfDb
{
    public class PurchaseOrderConfiguratin : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany<PurchaseOrderLine>()
                .WithOne()
                .HasForeignKey(f => f.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne<Supplier>()
            //    .WithMany(p => p.PurchaseOrders)
            //    .HasForeignKey(p => p.Supplier )
            //.OnDelete(DeleteBehavior.Restrict);
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
        }
    }
}

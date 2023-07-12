using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderLine> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.ItemId });
            builder.HasOne(p => p.PurchaseOrder)
                .WithMany(p => p.PurchaseOrderLines)
                .HasForeignKey(x => x.OrderId)
                .HasPrincipalKey(o => o.Id)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Item)
                .WithMany(p => p.PurchaseOrderLines)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.Price)
                .HasPrecision(9, 2);
            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(p => p.TotalPrice)
                .HasPrecision(9, 2)
                .HasComputedColumnSql("[Price] * [Quantity]");
        }
    }
}

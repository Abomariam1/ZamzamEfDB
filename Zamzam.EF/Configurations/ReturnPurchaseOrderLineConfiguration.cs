using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace ZamzamEfDb
{
    public class ReturnPurchaseOrderLineConfiguration : IEntityTypeConfiguration<ReturnPurchaseOrderLine>
    {
        public void Configure(EntityTypeBuilder<ReturnPurchaseOrderLine> builder)
        {
            builder.HasKey(k => new { k.OrderId, k.ItemId });
            builder.HasOne<ReturnPurchaseOrder>()
                .WithMany(p => p.ReturnPurchaseOrderLines)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.Price)
                .HasPrecision(9, 2);
            builder.Property(p => p.TotalPrice)
                .HasPrecision(9, 2)
                .HasComputedColumnSql("[Price] * [Quantity]");
        }
    }
}

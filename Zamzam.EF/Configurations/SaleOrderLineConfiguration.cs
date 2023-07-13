using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class SaleOrderLineConfiguration : IEntityTypeConfiguration<SaleOrderLine>
    {
        public void Configure(EntityTypeBuilder<SaleOrderLine> builder)
        {
            builder.HasKey(x => new { x.ItemId, x.OrderId });

            builder.HasOne(p => p.SaleOrder)
                .WithMany(s => s.SaleOrderLines)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Item)
                .WithMany(p => p.OrderLines)
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Price)
                .HasPrecision(9, 2);

            builder.Property(t => t.TotalPrice)
                .HasPrecision(9, 2)
                .HasComputedColumnSql("[Price] * [Quantity]");
        }
    }
}

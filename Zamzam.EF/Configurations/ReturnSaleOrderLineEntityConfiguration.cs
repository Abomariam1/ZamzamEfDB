using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class ReturnSaleOrderLineEntityConfiguration : IEntityTypeConfiguration<ReturnSaleOrderLine>
    {
        public void Configure(EntityTypeBuilder<ReturnSaleOrderLine> builder)
        {
            builder.HasKey(k => new { k.OrderId, k.ItemId });
            builder.HasOne<SaleOrder>()
                .WithMany()
                .HasForeignKey(p => p.OrderId)
                .HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.SaleOrderLine)
                .WithOne()
                .HasForeignKey<ReturnSaleOrderLine>(p => p.ItemId)
                .HasPrincipalKey<SaleOrderLine>(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(t => t.TotalPrice)
                .HasComputedColumnSql("[Price] * [Quantity]")
                .HasColumnType("decimal(9,2)");
        }
    }
}

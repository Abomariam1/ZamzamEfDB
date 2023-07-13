using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class ReturnSaleOrderLineEntityConfiguration : IEntityTypeConfiguration<ReturnSaleOrderLine>
    {
        public void Configure(EntityTypeBuilder<ReturnSaleOrderLine> builder)
        {
            builder.HasOne(p => p.SaleOrderLine)
                .WithOne(p => p.ReturnSaleOrderLine)
                .HasForeignKey<ReturnSaleOrderLine>(p => p.OrderId)
                .HasPrincipalKey<SaleOrderLine>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.SaleOrderLine)
                .WithOne(p => p.ReturnSaleOrderLine)
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

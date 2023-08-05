using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
using Zamzam.EF.Converters;

namespace Zamzam.EF
{
    public class SaleOrderLineConfiguration : IEntityTypeConfiguration<SaleOrderLine>
    {
        public void Configure(EntityTypeBuilder<SaleOrderLine> builder)
        {

            builder.Property(x => x.Id)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.ItemId)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.OrderId)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("uniqueidentifier");


            builder.HasKey(x => x.Id);
            builder.HasKey(x => new { x.ItemId, x.OrderId });

            builder.HasOne(p => p.SaleOrder)
                .WithMany(s => s.SaleOrderLines)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Item)
                .WithMany(p => p.SaleOrderLines)
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Price)
                .HasPrecision(9, 2);

            builder.Property(p => p.Discount)
                .HasPrecision(9, 2);

            builder.Property(t => t.TotalPrice)
                .HasPrecision(9, 2)
                .HasComputedColumnSql("([Price] * [Quantity]) - [Discount]");
        }
    }
}

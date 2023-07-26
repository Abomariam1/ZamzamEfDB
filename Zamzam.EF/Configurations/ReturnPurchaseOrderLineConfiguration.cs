using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
using Zamzam.EF.Converters;

namespace Zamzam.EF
{
    public class ReturnPurchaseOrderLineConfiguration : IEntityTypeConfiguration<ReturnPurchaseOrderLine>
    {
        public void Configure(EntityTypeBuilder<ReturnPurchaseOrderLine> builder)
        {

            builder.Property(x => x.Id)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("varchar(26)");

            builder.Property(x => x.OrderId).HasConversion(UlidToGuidValueConverter.ulidconverter).HasColumnType("varchar(26)");
            builder.Property(x => x.ItemId).HasConversion(UlidToGuidValueConverter.ulidconverter).HasColumnType("varchar(26)");


            builder.HasKey(x => x.Id);
            builder.HasKey(k => new { k.OrderId, k.ItemId, });

            builder.HasOne(r => r.ReturnPurcheseOrder)
                .WithMany(p => p.ReturnPurchaseOrderLines)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Item)
                .WithMany(p => p.ReturnPurchaseOrderLines)
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(p => p.Discount).HasPrecision(9, 2);
            builder.Property(p => p.TotalPrice).HasPrecision(9, 2)
                .HasComputedColumnSql("([Price] * [Quantity]) -[Discount]");
        }
    }
}

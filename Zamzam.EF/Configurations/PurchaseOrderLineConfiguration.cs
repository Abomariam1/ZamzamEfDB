using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
using Zamzam.EF.Converters;

namespace Zamzam.EF
{
    public class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderLine> builder)
        {

            builder.Property(x => x.Id)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("varchar(26)");

            builder.Property(x => x.OrderId)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("varchar(26)");

            builder.Property(x => x.ItemId)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("varchar(26)");


            builder.HasKey(x => x.Id);
            builder.HasKey(x => new { x.OrderId, x.ItemId });

            builder.HasOne(p => p.PurchaseOrder)
                .WithMany(p => p.PurchaseOrderLines)
                .HasForeignKey(x => x.OrderId)
                .HasPrincipalKey(o => o.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Item)
                .WithMany(p => p.PurchaseOrderLines)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(p => p.Discount).HasPrecision(9, 2);
            builder.Property(p => p.TotalPrice).HasPrecision(9, 2)
                .HasComputedColumnSql("([Price] * [Quantity]) -[Discount]");

        }
    }
}

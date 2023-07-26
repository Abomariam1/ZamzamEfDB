using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
using Zamzam.EF.Converters;

namespace Zamzam.EF
{
    public class MaintenanceOrderLineEntityConfiguration : IEntityTypeConfiguration<MaintenanceOrderLine>
    {
        public void Configure(EntityTypeBuilder<MaintenanceOrderLine> builder)
        {

            builder.Property(x => x.Id)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("varchar(26)");


            builder.Property(x => x.OrderId)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasMaxLength(24);

            builder.Property(x => x.ItemId)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasMaxLength(24);

            builder.HasKey(x => x.Id);
            builder.HasKey(x => new { x.OrderId, x.ItemId });

            builder.HasOne(m => m.Maintenance)
                .WithMany(m => m.MaintenanceOrderLines)
                .HasForeignKey(m => m.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Item)
                .WithMany(i => i.MaintenanceOrderLines)
                .HasForeignKey(i => i.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(c => c.Discount).HasPrecision(9, 2);
            builder.Property(t => t.TotalPrice).HasPrecision(9, 2)
                .HasComputedColumnSql("([Price] * [Quantity]) -[Discount]");

        }
    }
}

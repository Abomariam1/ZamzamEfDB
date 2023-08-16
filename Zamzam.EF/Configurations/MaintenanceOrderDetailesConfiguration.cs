using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class MaintenanceOrderDetailesConfiguration : IEntityTypeConfiguration<MaintenanceDetail>
    {
        public void Configure(EntityTypeBuilder<MaintenanceDetail> builder)
        {
            builder.ToTable(nameof(MaintenanceDetail));

            builder.HasKey(x => x.Id);
            //builder.HasKey(x => new { x.OrderId, x.ItemId });

            builder.HasOne(m => m.Maintenance)
                .WithMany(m => m.MaintenanceDetails)
                .HasForeignKey(m => m.MaintenanceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Item)
                .WithMany(i => i.MaintenanceDetails)
                .HasForeignKey(i => i.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(c => c.Discount).HasPrecision(9, 2);
            builder.Property(t => t.TotalPrice).HasPrecision(9, 2)
                .HasComputedColumnSql("([Price] * [Quantity]) -[Discount]");

        }
    }
}

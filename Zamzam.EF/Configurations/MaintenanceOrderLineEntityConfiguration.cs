using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class MaintenanceOrderLineEntityConfiguration : IEntityTypeConfiguration<MaintenanceOrderLine>
    {
        public void Configure(EntityTypeBuilder<MaintenanceOrderLine> builder)
        {
            //builder.HasKey(x => new { x.OrderId, x.ItemId });

            //builder.HasOne(m => m.Maintenance)
            //    .WithMany(m => m.MaintenanceOrderLines)
            //    .HasForeignKey(m => m.OrderId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(i => i.Item)
            //    .WithMany(i => i.MaintenanceOrderLines)
            //    .HasForeignKey(i => i.ItemId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

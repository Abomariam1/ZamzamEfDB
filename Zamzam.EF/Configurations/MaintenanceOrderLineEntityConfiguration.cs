using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace ZamzamEfDb
{
    public class MaintenanceOrderLineEntityConfiguration : IEntityTypeConfiguration<MaintenanceOrderLine>
    {
        public void Configure(EntityTypeBuilder<MaintenanceOrderLine> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Maintenance>(m => m.Maintenance)
                .WithMany(m => m.MaintenanceOrderLines)
                .HasForeignKey(m => m.MaintenanceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Item>(i => i.Item)
                .WithMany(i => i.MaintenanceOrderLines)
                .HasForeignKey(i => i.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

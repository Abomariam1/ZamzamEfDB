using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain;

namespace Zamzam.EF
{
    public class MaintenanceEntityConfiguration : IEntityTypeConfiguration<Maintenance>
    {
        public void Configure(EntityTypeBuilder<Maintenance> builder)
        {


            builder.HasOne(s => s.SaleOrder)
                .WithMany(x => x.Maintenances)
                .HasForeignKey(x => x.SaleOrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

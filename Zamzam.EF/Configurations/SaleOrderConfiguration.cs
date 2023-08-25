using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain.Entites;

namespace Zamzam.EF.Configurations
{
    public class SaleOrderConfiguration : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {
            builder.HasOne(c => c.Customer)
                .WithMany(c => c.SaleOrders)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

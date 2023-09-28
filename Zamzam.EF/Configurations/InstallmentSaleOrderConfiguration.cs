using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain;

namespace Zamzam.EF.Configurations
{
    public class InstallmentSaleOrderConfiguration : IEntityTypeConfiguration<InstallmentedSaleOrder>
    {
        public void Configure(EntityTypeBuilder<InstallmentedSaleOrder> builder)
        {

            builder.HasBaseType<Order>();

            builder.HasOne(c => c.Customer)
                .WithMany(c => c.InstallmentedSaleOrders)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(i => i.InstallmentValue)
                .HasPrecision(9, 2);

            builder.Property(p => p.Payed)
                .HasPrecision(9, 2);
            builder.Property(r => r.Remains)
                .HasPrecision(9, 2);
        }
    }
}

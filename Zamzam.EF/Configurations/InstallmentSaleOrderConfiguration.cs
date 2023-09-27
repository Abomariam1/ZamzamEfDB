using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain;

namespace Zamzam.EF.Configurations
{
    public class InstallmentSaleOrderConfiguration : IEntityTypeConfiguration<InstallmentedSaleOrder>
    {
        public void Configure(EntityTypeBuilder<InstallmentedSaleOrder> builder)
        {
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.InstallmentedSaleOrders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.InstallmentValue)
                .HasPrecision(9, 2);

            builder.Property(r => r.Payed)
                .HasPrecision(9, 2);
            builder.Property(r => r.Remains)
                .HasPrecision(9, 2);
        }
    }
}

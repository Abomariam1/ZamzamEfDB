using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF.Configurations
{
    public class InstallmentSaleOrderConfiguration : IEntityTypeConfiguration<InstallmentedSaleOrder>
    {
        public void Configure(EntityTypeBuilder<InstallmentedSaleOrder> builder)
        {

            builder.Property(r => r.InstallmentValue)
                .HasPrecision(9, 2);
        }
    }
}

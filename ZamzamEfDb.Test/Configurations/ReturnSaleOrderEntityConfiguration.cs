using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
namespace ZamzamEfDb.Test.Configurations
{
    public class ReturnSaleOrderEntityConfiguration : IEntityTypeConfiguration<ReturnSaleOrder>
    {
        public void Configure(EntityTypeBuilder<ReturnSaleOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.SaleOrder)
                .WithOne()
                .HasForeignKey<ReturnSaleOrder>(p => p.Id)
                .HasPrincipalKey<SaleOrder>(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.TotalPrice)
                .HasPrecision(9, 2);

            builder.Property(s => s.TotalDiscount)
                .HasPrecision(9, 2);

            builder.Property(s => s.Payed)
                .HasPrecision(9, 2);

            builder.Property(r => r.Remains)
                .HasPrecision(9, 2);

            builder.Property(s => s.NetPrice)
                .HasPrecision(9, 2)
                .HasComputedColumnSql("[TotalPrice] - [TotalDiscount]");
        }
    }
}

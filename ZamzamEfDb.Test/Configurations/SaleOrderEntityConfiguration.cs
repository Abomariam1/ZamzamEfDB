using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
namespace ZamzamEfDb.Test.Configurations
{
    public class SaleOrderEntityConfiguration : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {
            //builder.Property(c => c.Name).HasAnnotation("NotMaped");
            builder.HasKey(x => x.Id);
            builder.ToTable("SaleOrders");

            builder.Property(d => d.OrderDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.TotalPrice)
                .HasPrecision(9, 2);

            builder.Property(r => r.Remains)
                .HasPrecision(9, 2);

            builder.Property(s => s.TotalDiscount)
                .HasPrecision(9, 2);

            builder.Property(s => s.Payed)
                .HasPrecision(9, 2);

            builder.Property(s => s.NetPrice)
                .HasPrecision(9, 2)
                .HasComputedColumnSql("[TotalPrice] - [TotalDiscount]");

        }
    }
}

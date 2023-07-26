using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
using Zamzam.EF.Converters;

namespace Zamzam.EF
{
    public class ReturnSaleOrderEntityConfiguration : IEntityTypeConfiguration<ReturnSaleOrder>
    {
        public void Configure(EntityTypeBuilder<ReturnSaleOrder> builder)
        {

            builder.Property(x => x.Id)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("varchar(26)");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.SaleOrder)
                .WithMany(s => s.ReturnSaleOrder)
                .HasForeignKey(s => s.SaleOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Employee)
                .WithMany(s => s.ReturnSales)
                .HasForeignKey(p => p.EmployeeId)
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

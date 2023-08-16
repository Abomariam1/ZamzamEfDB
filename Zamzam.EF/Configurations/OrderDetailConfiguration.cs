using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.HasKey(x => new { x.OrderId, x.ItemId });

            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(p => p.Discount).HasPrecision(9, 2);
            builder.Property(p => p.TotalPrice).HasPrecision(9, 2)
                .HasComputedColumnSql("([Price] * [Quantity]) -[Discount]");

            builder.HasOne(p => p.Order)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(x => x.OrderId)
                .HasPrincipalKey(o => o.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Item)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

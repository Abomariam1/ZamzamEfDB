﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class ReturnSaleOrderLineEntityConfiguration : IEntityTypeConfiguration<ReturnSaleOrderLine>
    {
        public void Configure(EntityTypeBuilder<ReturnSaleOrderLine> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.ItemId });

            builder.HasOne(p => p.ReturnSaleOrder)
                .WithMany(p => p.returnSaleOrderLines)
                .HasForeignKey(p => p.ReturnSaleOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Item)
                .WithMany(p => p.ReturnSaleOrderLines)
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(p => p.Discount).HasPrecision(9, 2);
            builder.Property(t => t.TotalPrice)
                .HasComputedColumnSql("([Price] * [Quantity]) - [Discount]")
                .HasPrecision(9, 2);
        }
    }
}
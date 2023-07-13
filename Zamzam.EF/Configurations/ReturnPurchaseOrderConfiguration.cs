﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class ReturnPurchaseOrderConfiguration : IEntityTypeConfiguration<ReturnPurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<ReturnPurchaseOrder> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(p => p.PurchaseOrder)
                .WithOne(p => p.ReturnPurchaseOrder)
                .HasForeignKey<ReturnPurchaseOrder>(p => p.Id)
                .HasPrincipalKey<PurchaseOrder>(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

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

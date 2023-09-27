﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain;
using Zamzam.Domain.Types;

namespace Zamzam.EF.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasKey(x => x.Id);
            builder.UseTptMappingStrategy();
            builder.Property(x => x.InvoiceType)
                .HasConversion(
                x => x.ToString(),
                x => (InvoiceType)Enum.Parse(typeof(InvoiceType), x))
                .HasMaxLength(10);

            builder.Property(x => x.OrderType)
                .HasConversion(
                x => x.ToString(),
                x => (OrderType)Enum.Parse(typeof(OrderType), x))
                .HasMaxLength(20);

            builder.HasOne(o => o.Employee)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.EmployeeId);

            builder.Property(d => d.OrderDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.TotalPrice)
                .HasPrecision(9, 2);
            builder.UseTptMappingStrategy();

            builder.Property(t => t.TotalDiscount)
                .HasPrecision(9, 2);
            builder.UseTptMappingStrategy();

            builder.Property(s => s.NetPrice)
                .HasPrecision(9, 2)
                .HasComputedColumnSql("[TotalPrice] - [TotalDiscount]");

        }
    }
}

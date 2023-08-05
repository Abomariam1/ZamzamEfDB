﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class InstallmentEntityConfiguration : IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> builder)
        {

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier");

            builder.HasKey(i => i.Id);

            builder.Property(p => p.Value)
                .HasPrecision(9, 2);
            builder.Property(d => d.PayedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(s => s.SalesOrder)
                .WithMany(p => p.Installments)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Customer)
                .WithMany(p => p.Installments)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Employee)
                .WithMany(p => p.Installments)
                .HasForeignKey(i => i.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

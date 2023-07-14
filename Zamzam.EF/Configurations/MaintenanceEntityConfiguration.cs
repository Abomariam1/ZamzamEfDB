﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class MaintenanceEntityConfiguration : IEntityTypeConfiguration<Maintenance>
    {
        public void Configure(EntityTypeBuilder<Maintenance> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasOne(c => c.Customer)
            //    .WithMany(x => x.Maintenances)
            //    .HasForeignKey(x => x.CustomerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne<Employee>(e => e.Employee)
            //    .WithMany(x => x.Maintenances).
            //    HasForeignKey(x => x.EmployeeId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(s => s.SaleOrder)
            //    .WithMany(x => x.Maintenances)
            //    .HasForeignKey(x => x.SaleOrderId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

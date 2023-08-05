﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
using Zamzam.EF.Converters;

namespace Zamzam.EF
{
    public class EmployeeEntityConfeguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.Property(x => x.Id)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("uniqueidentifier");

            builder.Property(n => n.Name).IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Phone).HasMaxLength(15);

            builder.Property(a => a.Address).HasMaxLength(150);
            builder.Property(a => a.City).HasMaxLength(30);
            builder.Property(a => a.Region).HasMaxLength(30);
            builder.Property(a => a.Country).HasMaxLength(15);
            builder.Property(a => a.PostalCode).HasMaxLength(15);
            builder.Property(a => a.Qualification).HasMaxLength(100);
            builder.Property(a => a.Titel).HasMaxLength(30);
            builder.Property(a => a.Salary).HasPrecision(7, 2);

            builder.HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


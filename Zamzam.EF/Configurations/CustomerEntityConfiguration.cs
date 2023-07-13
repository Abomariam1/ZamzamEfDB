﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(n => n.Name).IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Phone).HasMaxLength(15);
            builder.Property(a => a.Address)
                .HasMaxLength(150);
            builder.Property(n => n.Notes)
                .HasMaxLength(150);
            builder.HasOne(i => i.Area).WithMany();

        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
using Zamzam.EF.Converters;

namespace Zamzam.EF
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Area)
                .WithMany(a => a.Customers)
                .HasForeignKey(a => a.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Id)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("uniqueidentifier");

            builder.Property(n => n.Name).IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Phone).HasMaxLength(15);
            builder.Property(a => a.Address)
                .HasMaxLength(150);
            builder.Property(n => n.Notes)
                .HasMaxLength(150);
        }
    }
}
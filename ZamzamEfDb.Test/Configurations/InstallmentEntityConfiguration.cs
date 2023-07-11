using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace ZamzamEfDb.Test.Configurations
{
    public class InstallmentEntityConfiguration : IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> builder)
        {
            builder.HasKey(i => i.InstallmentId);

            builder.HasOne(p => p.SalesOrder)
                .WithMany(p => p.Installments)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Customer)
                .WithMany(p => p.Installments)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Employee)
                .WithMany(p => p.Installments)
                .HasForeignKey(i => i.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Value)
                .HasPrecision(9, 2);

        }
    }
}

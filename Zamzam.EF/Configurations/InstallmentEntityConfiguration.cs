using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain;

namespace Zamzam.EF
{
    public class InstallmentEntityConfiguration : IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(p => p.Value)
                .HasPrecision(9, 2);
            builder.Property(d => d.PayedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(s => s.InstallmentedSale)
                .WithMany(p => p.Installments)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Employee)
                .WithMany(p => p.Installments)
                .HasForeignKey(i => i.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain.Entites;
using Zamzam.Domain.Types;

namespace Zamzam.EF.Configurations;
public class SupplierOperationConfiguration: IEntityTypeConfiguration<SupplierOperations>
{
    public void Configure(EntityTypeBuilder<SupplierOperations> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(a => a.Supplier)
            .WithMany(a => a.SupplierOperations)
            .HasForeignKey(a => a.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Order)
            .WithMany(a => a.SupplierOperations)
            .HasForeignKey(a => a.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.OperationType)
            .HasConversion(
            x => x.ToString(),
            x => (OperationType)Enum.Parse(typeof(InvoiceType), x))
            .HasMaxLength(20);

        builder.Property(t => t.OldBalance)
            .HasPrecision(9, 2);

        builder.Property(t => t.NewBalance)
            .HasPrecision(9, 2);

        builder.Property(t => t.Value)
            .HasPrecision(9, 2);


    }
}

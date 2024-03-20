using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain;

namespace Zamzam.EF
{
    public class SupplierConfiguration: IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Address)
                .HasMaxLength(100);
            builder.Property(p => p.Phone)
                .HasMaxLength(15);

            builder.Property(t => t.Balance)
                .HasPrecision(9, 2);

        }
    }
}

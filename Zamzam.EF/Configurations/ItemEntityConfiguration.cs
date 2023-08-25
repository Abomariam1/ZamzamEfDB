using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain;

namespace Zamzam.EF
{
    public class ItemEntityConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(n => n.Name).IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.PurchasingPrice)
                .HasDefaultValue(0)
                .HasPrecision(9, 2);
            builder.Property(s => s.sellingCashPrice)
                .HasDefaultValue(0)
                .HasPrecision(9, 2);
            builder.Property(i => i.InstallmentPrice)
                .HasDefaultValue(0)
                .HasPrecision(9, 2);
            builder.Property(b => b.Balance)
                .HasDefaultValue(0);
        }
    }
}
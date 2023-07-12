using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace ZamzamEfDb
{
    public class ItemEntityConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
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


//public int Id { get; set; }
//public string Name { get; set; }
//public decimal PurchasingPrice { get; set; }
//public decimal sellingCashPrice { get; set; }
//public decimal InstallmentPrice { get; set; }
//public int Balance { get; set; }
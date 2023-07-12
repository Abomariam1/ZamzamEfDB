using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace ZamzamEfDb
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


//public int Id { get; set; }
//public string Name { get; set; } = "عميل افتراضي";
//public string Phone { get; set; } = string.Empty;
//public string Address { get; set; } = string.Empty;
//public int AreaId { get; set; }
//public string Notes { get; set; } = string.Empty;
//public bool IsProplem { get; set; } = false;
//public bool IsBlackList { get; set; } = false;
//public bool IsDeleted { get; set; } = false;
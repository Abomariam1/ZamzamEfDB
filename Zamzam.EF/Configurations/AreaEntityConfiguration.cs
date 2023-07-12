using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace ZamzamEfDb
{
    public class AreaEntityConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.Property(a => a.Name).IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Staion).IsRequired()
                .HasMaxLength(30);
        }
    }
}
//public int Id { get; set; }
//public string Name { get; set; }
//public string Staion { get; set; }
//public string? Location { get; set; }
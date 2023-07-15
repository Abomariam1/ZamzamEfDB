using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class AreaEntityConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.Name).IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Station).IsRequired()
                .HasMaxLength(30);
        }
    }
}

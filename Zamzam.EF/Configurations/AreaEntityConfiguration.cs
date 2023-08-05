using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
using Zamzam.EF.Converters;

namespace Zamzam.EF
{
    public class AreaEntityConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {

            builder.Property(x => x.Id)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("uniqueidentifier");
            builder.HasKey(x => x.Id);
            builder.Property(a => a.Name).IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Station).IsRequired()
                .HasMaxLength(30);
        }
    }
}

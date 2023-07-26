using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;
using Zamzam.EF.Converters;

namespace Zamzam.EF
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.Property(x => x.Id)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("varchar(26)");

            builder.Property(n => n.Name).IsRequired()
                .HasMaxLength(50);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier");

            builder.Property(n => n.Name).IsRequired()
                .HasMaxLength(50);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain;

namespace Zamzam.EF
{
    public class AreaEntityConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {

            //builder.Property(x => x.Id)
            //    .HasColumnType("uniqueidentifier");
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Areas)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => x.Id);
            builder.Property(a => a.Name).IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Station).IsRequired()
                .HasMaxLength(30);
        }
    }
}

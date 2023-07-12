using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core;

namespace ZamzamEfDb
{
    public class EmployeeEntityConfeguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(n => n.Name).IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Phone).HasMaxLength(15);
            builder.Property(a => a.Address).HasMaxLength(150);
        }
    }
}


//public int Id { get; set; }
//public string Name { get; set; }
//public string Phone { get; set; }
//public string Address { get; set; }
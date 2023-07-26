using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Core.Entites;
using Zamzam.EF.Converters;

namespace Zamzam.EF.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(x => x.Id)
                .HasConversion(UlidToGuidValueConverter.ulidconverter)
                .HasColumnType("varchar(26)");

            builder.HasKey(x => x.Id);
            builder.Property(n => n.UserName).HasMaxLength(50);
            builder.Property(n => n.Password).HasMaxLength(50);
            builder.Property(n => n.Email).HasMaxLength(150);
            builder.Property(n => n.Phone).HasMaxLength(15);
        }
    }
}

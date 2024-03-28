using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain.Entites;
using Zamzam.Domain.Types;

namespace Zamzam.EF.Configurations;
public class ItemOperationConfiguration: IEntityTypeConfiguration<ItemOperation>
{
    public void Configure(EntityTypeBuilder<ItemOperation> builder)
    {
        builder.HasKey(x => x.Id);
        //builder.HasKey(x => new { x.OrderId, x.ItemId, x.OperationType });

        builder.HasOne(i => i.Order)
            .WithMany(i => i.ItemOperations)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.Item)
            .WithMany(t => t.ItemOperations)
            .HasForeignKey(t => t.ItemId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(t => t.OperationType)
            .HasConversion(
            x => x.ToString(),
            x => (OperationType)Enum.Parse(typeof(OperationType), x)
            ).HasMaxLength(7);

        builder.Property(t => t.OrderType)
            .HasConversion(
            x => x.ToString(),
            x => (OrderType)Enum.Parse(typeof(OrderType), x)
            ).HasMaxLength(17);

    }
}

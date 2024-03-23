using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamzam.Domain;

namespace Zamzam.EF.Configurations;
public class ReturnPurchaseOrderConfiguration: IEntityTypeConfiguration<ReturnPurchaseOrder>
{
    public void Configure(EntityTypeBuilder<ReturnPurchaseOrder> builder)
    {
        builder.HasOne(x => x.Purchase)
            .WithMany(x => x.ReturnPurchaseOrders)
            .HasForeignKey(x => x.PurchaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Supplier)
            .WithMany(x => x.ReturnPurchaseOrders)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}

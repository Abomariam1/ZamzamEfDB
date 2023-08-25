using Zamzam.Domain.Entites;

namespace Zamzam.Domain
{
    public class PurchaseOrder : Order
    {
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}

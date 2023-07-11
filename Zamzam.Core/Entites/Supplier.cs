namespace Zamzam.Core
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual IList<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual IList<ReturnPurchaseOrder> ReturnPurchases { get; set; }

    }
}

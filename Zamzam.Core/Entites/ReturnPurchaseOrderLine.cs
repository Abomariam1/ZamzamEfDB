namespace Zamzam.Core
{
    public class ReturnPurchaseOrderLine : BaseOrderLine
    {

        public virtual ReturnPurchaseOrder ReturnPurcheseOrder { get; set; }
        public virtual Item Item { get; set; }

    }
}

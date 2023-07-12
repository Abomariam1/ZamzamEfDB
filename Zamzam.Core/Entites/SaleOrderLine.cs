namespace Zamzam.Core
{

    public class SaleOrderLine : BaseOrderLine
    {
        public virtual SaleOrder SaleOrder { get; set; }
        public virtual Item Item { get; set; }
        //public virtual ReturnSaleOrderLine ReturnSaleOrderLine { get; set; }
    }
}

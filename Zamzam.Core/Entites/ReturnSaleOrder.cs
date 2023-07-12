namespace Zamzam.Core
{
    public class ReturnSaleOrder : BaseOrder
    {
        public SaleOrder SaleOrder { get; set; }
        public Employee Employee { get; set; }
        public IList<ReturnSaleOrderLine> ReturnSaleOrderLines { get; set; }


    }
}

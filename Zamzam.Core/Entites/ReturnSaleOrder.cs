namespace Zamzam.Core
{
    public class ReturnSaleOrder : BaseOrder
    {
        public Ulid EmployeeId { get; set; }
        public Ulid SaleOrderId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public ICollection<ReturnSaleOrderLine> returnSaleOrderLines { get; set; }
    }
}

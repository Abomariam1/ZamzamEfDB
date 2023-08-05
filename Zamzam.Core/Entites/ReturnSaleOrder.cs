namespace Zamzam.Core
{
    public class ReturnSaleOrder : BaseOrder
    {
        public Guid EmployeeId { get; set; }
        public Guid SaleOrderId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public virtual ICollection<ReturnSaleOrderLine> returnSaleOrderLines { get; set; }
    }
}

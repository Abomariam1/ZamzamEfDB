namespace Zamzam.Core
{
    public class ReturnSaleOrder : BaseOrder
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public ICollection<ReturnSaleOrderLine> returnSaleOrderLines { get; set; }
    }
}

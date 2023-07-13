namespace Zamzam.Core
{
    public class SaleOrder : BaseOrder
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IList<ReturnSaleOrder> ReturnSaleOrder { get; set; }
        public virtual IList<SaleOrderLine> SaleOrderLines { get; set; }
        public virtual IList<Installment> Installments { get; set; }
        public virtual IList<Maintenance> Maintenances { get; set; }

    }
}

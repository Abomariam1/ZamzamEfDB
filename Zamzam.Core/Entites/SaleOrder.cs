namespace Zamzam.Core
{
    public class SaleOrder : BaseOrder
    {
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<ReturnSaleOrder> ReturnSaleOrder { get; set; }
        public virtual ICollection<SaleOrderLine> SaleOrderLines { get; set; }
        public virtual ICollection<Installment> Installments { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }


    }
}

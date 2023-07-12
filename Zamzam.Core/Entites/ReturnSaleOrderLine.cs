namespace Zamzam.Core
{

    public class ReturnSaleOrderLine : BaseOrderLine
    {
        public virtual ReturnSaleOrder ReturnSaleOrder { get; set; }
        public virtual SaleOrderLine SaleOrderLine { get; set; }
    }
}
//you have to validate the side effects from returnnig products such as instllment payment and maney remainning
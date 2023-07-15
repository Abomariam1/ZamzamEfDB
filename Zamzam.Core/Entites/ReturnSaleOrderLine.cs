namespace Zamzam.Core
{

    public class ReturnSaleOrderLine : BaseOrderLine
    {
        public int ReturnSaleOrderId { get; set; }
        public virtual ReturnSaleOrder ReturnSaleOrder { get; set; }
        public virtual Item Item { get; set; }
    }
}
//you have to validate the side effects from returnnig products such as instllment payment and maney remainning
namespace Zamzam.Domain.Entites
{
    public class SaleOrder : Order
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }


    }
}

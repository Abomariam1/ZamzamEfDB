namespace Zamzam.Core
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}

namespace Zamzam.Core
{

    public abstract class BaseOrder : IOrder
    {
        public int Id { get; set; }
        public DateOnly OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Payed { get; set; }
        public decimal Remains { get; set; }
        public int EmployeeId { get; set; }
        public bool IsInstallmented { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Employee Employee { get; set; }
    }
}

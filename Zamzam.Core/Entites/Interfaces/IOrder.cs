namespace Zamzam.Core
{
    internal interface IOrder
    {
        public DateOnly OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Payed { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Remains { get; set; }
        public bool IsInstallmented { get; set; }
        public bool IsDeleted { get; set; }
        public int EmployeeId { get; set; }
    }
}

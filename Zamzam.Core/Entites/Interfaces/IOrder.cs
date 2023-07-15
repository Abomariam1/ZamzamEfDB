namespace Zamzam.Core
{
    internal interface IOrder : IEntity
    {
        public DateOnly OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Payed { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Remains { get; set; }
        public bool IsInstallmented { get; set; }
        public bool IsDeleted { get; set; }
    }
}

using Zamzam.Domain.Common;

namespace Zamzam.Domain
{
    public class Maintenance : BaseEntity
    {
        public DateTime PrimitiveDateToChange { get; set; }
        public DateTime CarbonDateToChange { get; set; }
        public DateTime BlockDateToChange { get; set; }
        public DateTime PostDateToChange { get; set; }
        public DateTime CalsetDateToChange { get; set; }
        public DateTime InfraDateToChange { get; set; }
        public DateTime MimpreenDateToChange { get; set; }
        public bool IsMaintained { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}

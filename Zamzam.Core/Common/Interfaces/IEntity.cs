namespace Zamzam.Domain.Common.Interfaces
{
    public interface IEntity
    {
        public int Id { get; }
        public bool IsDeleted { get; }
    }
}

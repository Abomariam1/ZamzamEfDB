namespace Zamzam.Core
{
    public interface IEntity
    {
        public int Id { get; }
        public bool IsDeleted { get; }
    }
}

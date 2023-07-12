namespace Zamzam.Core
{
    public interface IDataService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<T> Delete(int id);
    }
}

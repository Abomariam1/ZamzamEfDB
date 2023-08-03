namespace Zamzam.Core
{
    public interface IDataService<T> where T : class
    {

        IEnumerable<T> GetAll();
        T GetById(Ulid id);
        T Create(T entity);
        T Update(Ulid id, T entity);
        bool Delete(Ulid id);
        T FindByName(string name);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Ulid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(Ulid id, T entity);
        Task<bool> DeleteAsync(Ulid id);
        Task<T> FindByNameAsync(string name);
    }
}

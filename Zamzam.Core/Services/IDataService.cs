using Zamzam.Core.Entites;

namespace Zamzam.Core
{
    public interface IDataService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Ulid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(Ulid id, T entity);
        Task<bool> DeleteAsync(Ulid id);
        Task<User> FindByNameAsync(string name);
    }
}

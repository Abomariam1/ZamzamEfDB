namespace Zamzam.Core
{
    public interface IDataService<T> where T : class
    {

        IEnumerable<T> GetAll();
        T GetById(Guid id);
        T Create(T entity);
        T Update(Guid id, T entity);
        bool Delete(Guid id);
        T FindByName(string name);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(Guid id, T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<T> FindByNameAsync(string name);
    }
}

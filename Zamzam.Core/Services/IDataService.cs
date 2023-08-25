namespace Zamzam.Domain
{
    public interface IDataService<T> where T : class
    {

        IEnumerable<T> GetAll();
        T GetById(int id);
        T Create(T entity);
        T Update(int id, T entity);
        bool Delete(int id);
        T FindByName(string name);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
        Task<T> FindByNameAsync(string name);
    }
}

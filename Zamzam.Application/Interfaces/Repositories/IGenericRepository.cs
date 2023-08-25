using Zamzam.Domain.Common.Interfaces;

namespace Zamzam.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class, IAuditableEntity
    {
        IQueryable<T> Entities { get; }
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string name);
        Task AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
    }
}

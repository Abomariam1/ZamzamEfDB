using Microsoft.EntityFrameworkCore;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain.Common;

namespace Zamzam.EF.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
    {
        private readonly ZamzamDbContext _dbContext;

        public GenericRepository(ZamzamDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public Task<T> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<T>.AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain.Common;

namespace Zamzam.EF.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }


        public async Task<T> UpdateAsync(T entity)
        {
            T exist = await _dbContext.Set<T>().FindAsync(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return exist;
        }

        public async Task<T> DeleteAsync(int id)
        {
            T entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Set<T>().Remove(entity);
            return entity;
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

        public async Task<T> GetByNameAsync(string name) =>
            await _dbContext.Set<T>().FindAsync(name);

        //public async Task<T> AddAsync(T entity)
        //{
        //    await _dbContext.Set<T>().AddAsync(entity);
        //}

        //public async Task<int> DeleteAsync(int id)
        //{
        //    var entity = await _dbContext.Set<T>().FindAsync(id);
        //    _dbContext.Set<T>().Remove(entity);
        //    return entity.Id;
        //}
    }
}

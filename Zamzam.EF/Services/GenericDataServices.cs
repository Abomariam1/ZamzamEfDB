using Zamzam.Core;

namespace Zamzam.EF
{
    public class GenericDataServices<T> : IDataService<T> where T : class
    {
        private readonly ZamzamDbContextFactory _dbContextFactory;

        public GenericDataServices(ZamzamDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                var CreatedEntity = await context.Set<T>().AddAsync(entity);
                return CreatedEntity.Entity;
            }
        }

        public async Task<T> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Zamzam.Core;

namespace Zamzam.EF
{
    public class GenericDataServices<T> : IDataService<T> where T : BaseEntity
    {
        private readonly ZamzamDbContextFactory _dbContextFactory;

        public GenericDataServices(ZamzamDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public T Create(T entity)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                EntityEntry<T>? CreatedEntity = context.Set<T>().Add(entity);
                context.SaveChanges();
                return CreatedEntity.Entity;
            }
        }

        public T Update(int id, T entity)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                context.SaveChanges();
                return entity;
            }
        }
        public bool Delete(int id)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                T? entity = context.Set<T>().FirstOrDefault(predicate: (e) => e.Id == id);
                if (entity != null)
                {
                    entity.Id = id;
                    context.Set<T>().Remove(entity);
                    context.SaveChanges();
                }

                return true;
            }
        }
        public IEnumerable<T> GetAll()
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = context.Set<T>().ToList();
                return entities;
            }
        }

        public T GetById(int id)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                T? entities = context.Set<T>().FirstOrDefault((e) => e.Id == id);
                return entities;
            }
        }

        public T FindByName(string name)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> DeleteAsync(int id)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                T? entity = await context.Set<T>().FirstOrDefaultAsync(predicate: (e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }


        public async Task<T> CreateAsync(T entity)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                EntityEntry<T>? CreatedEntity = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return CreatedEntity.Entity;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                T? entities = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entities;
            }

        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        Task<T> IDataService<T>.FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}

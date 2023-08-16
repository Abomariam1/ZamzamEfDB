using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Zamzam.Core;
using Zamzam.Core.Entites;

namespace Zamzam.EF.Services
{
    public class ItemDataService : IDataService<Item>
    {
        private readonly ZamzamDbContextFactory _dbContextFactory;

        public ItemDataService(ZamzamDbContextFactory context)
        {
            _dbContextFactory = context;
        }

        public Item Create(Item entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> CreateAsync(Item entity)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                EntityEntry<Item>? CreatedEntity = await context.Set<Item>().AddAsync(entity);
                await context.SaveChangesAsync();
                return CreatedEntity.Entity;
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                Item entity = await context.Set<Item>().FirstOrDefaultAsync(predicate: ((e) => e.Id == id));
                context.Set<Item>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public Item FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<Item> entities = await context.Set<Item>().ToListAsync();
                return entities;
            }
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<Item> entities = await context.Set<Item>().ToListAsync();
                return entities;
            }
        }

        public Item GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                Item? entities = context.Set<Item>().FirstOrDefaultAsync((e) => e.Id == id).Result;
                return entities;
            }
        }

        public Item Update(int id, Item entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> UpdateAsync(int id, Item entity)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Set<Item>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        Task<Item> IDataService<Item>.FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Item> IDataService<Item>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

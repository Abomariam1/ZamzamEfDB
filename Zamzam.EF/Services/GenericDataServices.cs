﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Zamzam.Core;
using Zamzam.Core.Entites;

namespace Zamzam.EF
{
    public class GenericDataServices<T> : IDataService<T> where T : BaseEntity
    {
        private readonly ZamzamDbContextFactory _dbContextFactory;

        public GenericDataServices(ZamzamDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
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

        public async Task<bool> DeleteAsync(Ulid id)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync(predicate: ((e) => e.Id == id));
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public Task<User> FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> GetByIdAsync(Ulid id)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                T? entities = context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id).Result;
                return entities;
            }

        }

        public async Task<T> UpdateAsync(Ulid id, T entity)
        {
            using (ZamzamDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}

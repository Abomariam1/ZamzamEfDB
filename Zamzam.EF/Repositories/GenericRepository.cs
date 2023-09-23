﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            T exist = await _dbContext.Set<T>().FindAsync(entity.Id) ?? throw new ArgumentException();

            EntityEntry<T>? updated = _dbContext.Set<T>().Update(entity);

            return updated.Entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            T exist = await _dbContext.Set<T>().FindAsync(id) ?? throw new ArgumentException();
            exist.IsDeleted = true;
            EntityEntry<T>? updated = _dbContext.Set<T>().Update(exist);
            return updated.Entity;
        }

        public async Task<List<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public async Task<T> GetByNameAsync(string name) => await _dbContext.Set<T>().FindAsync(name);
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain.Common;

namespace Zamzam.EF.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity, new()
    {
        private readonly ApplicationDbContext _dbContext;
        public IQueryable<T> Entities => _dbContext.Set<T>();

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


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
            T? exist = await _dbContext.Set<T>().FindAsync(id);
            exist.IsDeleted = true;
            EntityEntry<T>? updated = _dbContext.Set<T>().Update(exist);
            return updated.Entity;
        }

        public async Task<List<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public async Task<T> GetByNameAsync(Expression<Func<T, bool>> criteria)
        {
            T? entity = await _dbContext.Set<T>().SingleOrDefaultAsync(criteria);
            if (entity == null)
                return null;
            return entity;
        }
    }
}

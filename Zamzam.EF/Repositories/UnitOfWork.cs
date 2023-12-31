﻿using System.Collections;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain.Common;
using Zamzam.Domain.Entites;

namespace Zamzam.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable _repositories;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity
        {
            _repositories ??= new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }
            IGenericRepository<T>? repository = (IGenericRepository<T>)_repositories[type];
            return (IGenericRepository<T>)_repositories[type];
        }
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            _disposed = true;
        }
        public Task RollBack()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public void RecreateCleanDatabase()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }
        public Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            throw new NotImplementedException();
        }

        public ISaleOrderRepository? SaleOrderRepository()
        {
            _repositories ??= new Hashtable();

            var type = typeof(SaleOrder).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(SaleOrderRepository);

                var repositoryInstance = Activator.CreateInstance(repositoryType, _dbContext);

                _repositories.Add(type, repositoryInstance);
            }
            //IGenericRepository<T>? repository = (IGenericRepository<T>)_repositories[type];
            return _repositories[type] as ISaleOrderRepository;

        }
    }
}

using System.Collections;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain.Common;
using Zamzam.Domain.Entites;

namespace Zamzam.EF.Repositories
{
    public class UnitOfWork(ApplicationDbContext dbContext): IUnitOfWork
    {
        private Hashtable _repositories = [];
        private bool _disposed;

        public IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity
        {
            //_repositories ??= new Hashtable();

            var type = typeof(T).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), dbContext);

                _repositories.Add(type, repositoryInstance);
            }
            //IGenericRepository<T>? repository = (IGenericRepository<T>)_repositories[type]!;
            return (IGenericRepository<T>)_repositories[type]!;
        }
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if(_disposed)
            {
                if(disposing)
                {
                    //dispose managed resources
                    dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            _disposed = true;
        }
        public Task RollBack()
        {
            dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
        public void RecreateCleanDatabase()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
        public Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            throw new NotImplementedException();
        }

        public ISaleOrderRepository SaleOrderRepository()
        {
            _repositories ??= new Hashtable();

            string? type = typeof(SaleOrder).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(SaleOrderRepository);

                var repositoryInstance = Activator.CreateInstance(repositoryType, dbContext);

                _repositories.Add(type, repositoryInstance);
            }
            //IGenericRepository<T>? repository = (IGenericRepository<T>)_repositories[type];
            return (ISaleOrderRepository)_repositories[type]!;

        }
    }
}

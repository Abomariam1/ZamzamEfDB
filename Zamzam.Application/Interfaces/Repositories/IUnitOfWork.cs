using Zamzam.Domain.Common;

namespace Zamzam.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;
        ISaleOrderRepository SaleOrderRepository();
        Task<int> Save(CancellationToken cancellationToken);
        Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
        void RecreateCleanDatabase();
        Task RollBack();
    }
}

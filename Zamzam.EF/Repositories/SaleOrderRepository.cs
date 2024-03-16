using System.Linq.Expressions;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain.Entites;

namespace Zamzam.EF.Repositories
{
    internal class SaleOrderRepository: ISaleOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleOrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<SaleOrder> Entities => throw new NotImplementedException();

        public async Task<SaleOrder> AddAsync(SaleOrder entity)
        {
            await _dbContext.SaleOrders.AddAsync(entity);
            return entity;
        }

        public Task<SaleOrder> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SaleOrder>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SaleOrder> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SaleOrder> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<SaleOrder> GetByNameAsync(Expression<Func<SaleOrder, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public Task<SaleOrder> UpdateAsync(SaleOrder entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangAsync(SaleOrder[] entity) => throw new NotImplementedException();
    }
}

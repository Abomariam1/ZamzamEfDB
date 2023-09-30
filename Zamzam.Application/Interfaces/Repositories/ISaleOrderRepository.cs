using Zamzam.Domain.Entites;

namespace Zamzam.Application.Interfaces.Repositories
{
    public interface ISaleOrderRepository : IGenericRepository<SaleOrder>
    {
        Task<SaleOrder> GetByNameAsync(string name);
    }
}

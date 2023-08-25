using Microsoft.EntityFrameworkCore;
using Zamzam.Shared;

namespace Zamzam.Application.Extentions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(
            this IQueryable<T> source,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken) where T : class
        {
            if (source == null)
                return null;
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 1 : pageSize;
            int count = await source.CountAsync(cancellationToken);
            List<T> itmes = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return PaginatedResult<T>.Create(itmes, count, pageNumber, pageSize);
        }
    }
}

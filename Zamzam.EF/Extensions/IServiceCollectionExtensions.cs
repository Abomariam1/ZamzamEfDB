using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.EF.Repositories;

namespace Zamzam.EF.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddEfLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();
        }
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ZamzamDbContext>(options =>
               options.UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(ZamzamDbContext).Assembly.FullName)));
        }
        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Zamzam.Application.Extentions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMediator();
            services.AddValidators();
        }
        private static void AddAutoMapper(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        private static void AddMediator(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        private static void AddValidators(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

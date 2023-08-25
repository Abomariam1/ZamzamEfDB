using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Zamzam.Application.Extentions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapperٍ(Assembly.GetExecutingAssembly());
            services.AddMediator();
            services.AddValidators();
        }
        private static void AddAutoMapperٍ(this IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapperٍ(assembly);
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

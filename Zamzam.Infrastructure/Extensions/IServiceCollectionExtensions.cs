using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Zamzam.Application.Interfaces;
using Zamzam.Domain.Common;
using Zamzam.Domain.Common.Interfaces;
using Zamzam.Infrastructure.Services;

namespace Zamzam.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }
        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<IEmailService, EmailService>();
        }
    }
}

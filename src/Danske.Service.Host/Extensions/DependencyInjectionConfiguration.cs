using Danske.Service.Host.Mappers;
using Danske.Service.Services;
using Danske.Service.Services.Adapters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Danske.Service.Host.Extensions
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<ICalculationService, CachedCalculationService>(provider =>
            {
                var memoryCache = provider.GetService<IMemoryCache>();
                var calculationService = new CalculationService();

                return new CachedCalculationService(calculationService, memoryCache);
            });
            services.AddSingleton<IGraphMapper, GraphMapper>();

            return services;
        }
    }
}

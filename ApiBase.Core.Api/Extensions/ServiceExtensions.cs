using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBase.Core.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBaseApiControllers(this IServiceCollection services)
        {
            services.AddControllers().ConfigureApplicationPartManager(apm =>
                {
                    var assembly = typeof(ServiceExtensions).Assembly;
                    var partFactory = ApplicationPartFactory.GetApplicationPartFactory(assembly);
                    
                    foreach (var part in partFactory.GetApplicationParts(assembly))
                    {
                        apm.ApplicationParts.Add(part);
                    }
                });
            return services;
        }
    }
}

using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repository;

namespace Repository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseBackend<TContext>(this IServiceCollection services)
               where TContext : DbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<DbContext, TContext>();
            return services;
        }
    }
}

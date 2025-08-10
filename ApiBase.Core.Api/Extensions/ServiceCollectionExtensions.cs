using ApiBase.Core.Domain.Interfaces;
using ApiBase.Core.Infra.UnitOfWork;
using ApiBase.Core.Repositories.Repositories.Repository;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApiBase.Core.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiBase<TDbContext>(
         this IServiceCollection services,
         Action<IServiceCollection, IConfiguration> registerServices,
         Action<DbContextOptionsBuilder> dbOptions)
         where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(dbOptions);

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            registerServices?.Invoke(services, null);

            services.AddControllers()
                .ConfigureApplicationPartManager(apm =>
                {
                    apm.ApplicationParts.Add(new AssemblyPart(typeof(ServiceCollectionExtensions).Assembly));

                    apm.ApplicationParts.Add(new AssemblyPart(typeof(TDbContext).Assembly));
                });

            return services;
        }
    }
}
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces.DbContexts;
using Infrastructure.Repositories.AccountRepositories;
using Application.Common.Interfaces.Repositories.AccountRepositories;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<ITenantDBContext, TenantDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Platform"),b => b.MigrationsAssembly(typeof(TenantDbContext).Assembly.FullName) )
            );
            services.AddTransient<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}

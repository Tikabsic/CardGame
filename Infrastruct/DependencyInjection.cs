using Application.Data;
using Application.Interfaces;
using Application.Interfaces.InfrastructureRepositories;
using Infrastruct.Persistence;
using Infrastruct.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace Infrastruct
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CardGameDatabase")));


            //Infrastructure
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAppDbContext, AppDbContext>();

            //services.AddLogging(loggingBuilder =>
            //{
            //    loggingBuilder.ClearProviders();
            //    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            //    loggingBuilder.AddNLog(configuration);
            //});

            //Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}

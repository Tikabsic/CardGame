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

            //Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            return services;
        }
    }
}

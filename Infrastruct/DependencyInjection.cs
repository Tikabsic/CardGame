using Application.Data;
using Application.Interfaces;
using Application.Interfaces.InfrastructureRepositories;
using Infrastruct.Persistence;
using Infrastruct.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Infrastruct
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CardGameDatabase")));

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            //Infrastructure
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAppDbContext, AppDbContext>();

            var provider = services.BuildServiceProvider();
            var dbContext = provider.GetService<AppDbContext>();

            var cardsSeeder = new CardsSeeder(dbContext);

            if (!dbContext.Cards.Any())
            {
                cardsSeeder.SeedCards();
            }

            //Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IDeckRepository, DeckRepository>();
            services.AddScoped<ICardsRepository, CardsRepository>();
            services.AddScoped<IStackRepository, StackRepository>();

            return services;
        }
    }
}

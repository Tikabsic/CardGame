using Domain.Entities.CardEntities;
using Domain.Entities.RoomEntities;
using Domain.EntityInterfaces;
using Domain.EntityServices;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {

            services.AddScoped<IStackEntityService, StackEntityService>();
            services.AddScoped<IRoomEntityService, RoomEntityService>();

            services.AddScoped<Stack>();
            services.AddScoped<Room>();

            return services;
        }
    }
}

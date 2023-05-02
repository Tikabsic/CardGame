using Domain.EntityServices;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {

            services.AddScoped<IStackEntityService, StackEntityService>();
            services.AddScoped<IRoomEntityService, RoomEntityService>();

            return services;
        }
    }
}

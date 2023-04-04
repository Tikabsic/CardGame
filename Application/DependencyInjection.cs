using Application.Interfaces.Services;
using Application.Middleware;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            //Services
            services.AddScoped<IAccountService, AccountService>();

            //Tools
            services.AddValidatorsFromAssembly(assembly);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Middlewares
            services.AddScoped<ErrorHandlingMiddleware>();

            return services;
        }
    }
}

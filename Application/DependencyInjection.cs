using Application.Authentication;
using Application.AutoMapper;
using Application.DTO;
using Application.DTO.Validators;
using Application.Interfaces.Services;
using Application.Middleware;
using Application.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            var authenticationSettings = new AuthenticationSettings();

            configuration.GetSection("Authentication").Bind(authenticationSettings);

            //Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserValidator>();
            services.AddSingleton(authenticationSettings);
            services.AddHttpContextAccessor();
            services.AddSignalR();

            //Tools
            services.AddAutoMapper(Assembly.GetAssembly(typeof(UserMappingProfile)));


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JWTIssuer,
                    ValidAudience = authenticationSettings.JWTIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JWTKey)),
                };
            });


            //Middlewares
            services.AddScoped<ErrorHandlingMiddleware>();

            return services;
        }
    }
}

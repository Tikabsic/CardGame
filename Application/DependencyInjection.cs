using Application.Authentication;
using Application.AutoMapper;
using Application.DTO;
using Application.DTO.Validators;
using Application.Hubs;
using Application.Interfaces.Services;
using Application.Middleware;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

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
            services.AddScoped<IPlayerService, PlayerService>();

            services.AddSingleton(authenticationSettings);

            services.AddSignalR().AddHubOptions<GameRoomHub>(options =>
            {
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(3600);
            }).AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            //Tools
            services.AddAutoMapper(Assembly.GetAssembly(typeof(UserMappingProfile)));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JWTIssuer,
                    ValidAudience = authenticationSettings.JWTIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JWTKey)),
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/Room") || path.StartsWithSegments("/Main")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });


            //Middlewares
            services.AddScoped<ErrorHandlingMiddleware>();

            return services;
        }
    }
}

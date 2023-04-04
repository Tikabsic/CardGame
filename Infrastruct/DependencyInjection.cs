﻿using Application.Data;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Services;
using Infrastruct.Persistence;
using Infrastruct.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;


namespace Infrastruct
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CardGameDatabase")));

            services.AddScoped<IAppDbContext, AppDbContext>();

            //Infrastructure
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();


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

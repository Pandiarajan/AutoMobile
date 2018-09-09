﻿using AutoApi.Controllers;
using AutoMapper;
using AutoRepository;
using Microsoft.Extensions.DependencyInjection;

namespace AutoApi.Config
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICarsController, CarsController>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IDataStore, InMemoryDataStore>();
            return services;
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
    }
}

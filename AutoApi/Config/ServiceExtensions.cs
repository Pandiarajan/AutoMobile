using AutoApi.Controllers;
using AutoRepository;
using Microsoft.Extensions.DependencyInjection;

namespace AutoApi.Config
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            return services.AddScoped<ICarsController, CarsController>()
                .AddScoped<ICarRepository, CarRepository>()
                .AddScoped<IDataStore, InMemoryDataStore>();
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            return services;
        }
    }
}

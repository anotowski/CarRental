using System;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Api.DependencyInjection
{
    public static class ExternalToolsServices
    {
        public static IServiceCollection AddExternalTools(this IServiceCollection services)
        {
            // Swagger
            services.AddSwaggerGen();

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}

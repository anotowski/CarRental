using CarRental.BusinessLogic.Generators;
using CarRental.BusinessLogic.Generators.Interfaces;
using CarRental.Database.Repositories;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services;
using CarRental.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Api.DependencyInjection
{
    public static class CarRentalServices
    {
        public static IServiceCollection AddCarRentalServices(this IServiceCollection services)
        {
            services.AddScoped<ICarRentalService, CarRentalService>();
            services.AddScoped<ICarRentalRepository, CarRentalRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBookingNumberGenerator, BookingNumberGenerator>();
            services.AddScoped<IRentService, RentService>();

            return services;
        }
    }
}

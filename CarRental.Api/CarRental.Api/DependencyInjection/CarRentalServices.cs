using CarRental.BusinessLogic;
using CarRental.BusinessLogic.Generators;
using CarRental.BusinessLogic.Generators.Interfaces;
using CarRental.BusinessLogic.Interfaces;
using CarRental.Database.Repositories;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services;
using CarRental.Services.Interfaces;
using CarRental.Services.Managers;
using CarRental.Services.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Api.DependencyInjection
{
    public static class CarRentalServices
    {
        public static IServiceCollection AddCarRentalServices(this IServiceCollection services)
        {
            services.AddScoped<ICarRentalService, CarRentalService>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IRentalHistoryRepository, RentalHistoryRepository>();
            services.AddScoped<ICarRetrieveManager, CarRetrieveManager>();
            services.AddScoped<ICustomerRetrieveManager, CustomerRetrieveManager>();
            services.AddScoped<IRentalHistoryManager, RentalHistoryManager>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBookingNumberGenerator, BookingNumberGenerator>();
            services.AddScoped<IRentService, RentService>();
            services.AddScoped<IPaymentCalculatorFactory, PaymentCalculatorFactory>();
            services.AddScoped<IReturnService, ReturnService>();

            return services;
        }
    }
}

using CarRental.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarRental.Database
{
    public class CarRentalContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public CarRentalContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<RentalHistory> RentalHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasIndex(c => c.PlateNumber)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<Car>()
                .HasData(
                    new Car
                    {
                        Id = 1,
                        Brand = "Volvo",
                        ModelName = "S90",
                        Category = CategoryType.Premium,
                        IsAvailable = true,
                        BaseDayRentalFee = 120m,
                        KilometerFee = 5m,
                        PlateNumber = "CarRental001",
                        CurrentMileage = 0,
                    },
                    new Car
                    {
                        Id = 2,
                        Brand = "BMW",
                        ModelName = "M8 Competition",
                        Category = CategoryType.Premium,
                        IsAvailable = true,
                        BaseDayRentalFee = 125m,
                        KilometerFee = 5.65m,
                        PlateNumber = "CarRental002",
                        CurrentMileage = 0,
                    },
                    new Car
                    {
                        Id = 3,
                        Brand = "Hyundai",
                        ModelName = "i30",
                        Category = CategoryType.Compact,
                        IsAvailable = true,
                        BaseDayRentalFee = 55m,
                        KilometerFee = 3.99m,
                        PlateNumber = "CarRental003",
                        CurrentMileage = 0,
                    },
                    new Car
                    {
                        Id = 4,
                        Brand = "Volkswagen",
                        ModelName = "T6 California",
                        Category = CategoryType.Minivan,
                        IsAvailable = true,
                        BaseDayRentalFee = 68.99m,
                        KilometerFee = 3m,
                        PlateNumber = "CarRental004",
                        CurrentMileage = 0
                    }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _configuration.GetConnectionString("DbConnectionString"),
                b => b.MigrationsAssembly("CarRental.Database"));
        }
    }
}

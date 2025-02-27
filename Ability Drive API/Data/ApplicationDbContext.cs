using Ability_Drive_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ability_Drive_API.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for all models
        public DbSet<User> Users { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<BusSchedule> BusSchedules { get; set; }
        public DbSet<SeatBooking> SeatBookings { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<UserVoucher> UserVouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();
            // Seed initial data (optional)
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed initial users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "email@gmail.com",
                    PhoneNumber = "1234567890",
                    IsDisabled = false,
                    Password = "password123",
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@gmail.com",
                    PhoneNumber = "0987654321",
                    IsDisabled = true,
                    Password = "password321",
                    CreatedAt = new DateTime(2024, 2, 1)
                },
                new User
                {
                    Id = 3,
                    FirstName = "Bob",
                    LastName = "Johnson",
                    Email = "bob.johnson@gmail.com",
                    PhoneNumber = "1122334455",
                    IsDisabled = false,
                    Password = "password456",
                    CreatedAt = new DateTime(2024, 3, 1)
                }
            );

            // Seed initial drivers
            modelBuilder.Entity<Driver>().HasData(
                new Driver
                {
                    Id = 1,
                    Name = "Alice Smith",
                    Password = "pass#123",
                    LicenseNumber = "DRV12345",
                    PhoneNumber = "01134896510",
                    VehicleType = "Sedan",
                    VehicleRegistration = "ABC123",
                    Rating = 4.8m,
                    LastKnownLocation = "Cairo",
                    IsAvailable = true,
                    PreferredLocations = new List<string> { "Cairo", "Giza", "Alexandria" }
                },
                new Driver
                {
                    Id = 2,
                    Name = "Ali Mohammed",
                    Password = "pass#123",
                    LicenseNumber = "DRV12345",
                    PhoneNumber = "01111111111",
                    VehicleType = "Sedan",
                    VehicleRegistration = "ABC123",
                    Rating = 4.8m,
                    LastKnownLocation = "Giza",
                    IsAvailable = true,
                    PreferredLocations = new List<string> { "6th October", "Helwan", "Nasr City" }
                },
                new Driver
                {
                    Id = 3,
                    Name = "Sarah Johnson",
                    Password = "pass#789",
                    LicenseNumber = "DRV67890",
                    PhoneNumber = "0222333444",
                    VehicleType = "SUV",
                    VehicleRegistration = "XYZ789",
                    LastKnownLocation = "Alexandria",
                    Rating = 4.9m,
                    IsAvailable = true,
                    PreferredLocations = new List<string> { "Maadi", "Zamalek", "New Cairo" }
                }
            );

            // Seed initial bus schedules
            modelBuilder.Entity<BusSchedule>().HasData(
                new BusSchedule
                {
                    Id = 1,
                    BusNumber = "B101",
                    DepartureTime = new DateTime(2024, 1, 1).AddHours(1),
                    FromLocation = "City Center",
                    ToLocation = "North Station",
                    TotalNormalSeats = 20,
                    AvailableNormalSeats = 20,
                    TotalDisabledSeats = 2,
                    AvailableDisabledSeats = 2,
                    IsWheelchairAccessible = true,
                    Price = 50
                },
                new BusSchedule
                {
                    Id = 2,
                    BusNumber = "B102",
                    DepartureTime = new DateTime(2024, 2, 1).AddHours(2),
                    FromLocation = "East Station",
                    ToLocation = "West Station",
                    TotalNormalSeats = 30,
                    AvailableNormalSeats = 30,
                    TotalDisabledSeats = 3,
                    AvailableDisabledSeats = 3,
                    IsWheelchairAccessible = true,
                    Price = 50
                },
                new BusSchedule
                {
                    Id = 3,
                    BusNumber = "B103",
                    DepartureTime = new DateTime(2024, 3, 1).AddHours(3),
                    FromLocation = "South Station",
                    ToLocation = "Central Station",
                    TotalNormalSeats = 25,
                    AvailableNormalSeats = 25,
                    TotalDisabledSeats = 2,
                    AvailableDisabledSeats = 2,
                    IsWheelchairAccessible = true,
                    Price = 50
                }
            );

            // Seed initial rides
            modelBuilder.Entity<Ride>().HasData(
                new Ride
                {
                    Id = 1,
                    UserId = 1,
                    DriverId = 1,
                    PickupLocation = "Cairo",
                    Destination = "Giza",
                    Status = "Completed",
                    RequestTime = new DateTime(2024, 1, 1).AddHours(2),
                    Cost = 100m
                },
                new Ride
                {
                    Id = 2,
                    UserId = 2,
                    DriverId = 2,
                    PickupLocation = "Helwan",
                    Destination = "Nasr City",
                    Status = "Pending",
                    RequestTime = new DateTime(2024, 2, 1).AddHours(3),
                    Cost = 150m
                },
                new Ride
                {
                    Id = 3,
                    UserId = 3,
                    DriverId = 3,
                    PickupLocation = "Maadi",
                    Destination = "New Cairo",
                    Status = "Ongoing",
                    RequestTime = new DateTime(2024, 3, 1).AddHours(4),
                    Cost = 200m
                }
            );
        }
    }
}

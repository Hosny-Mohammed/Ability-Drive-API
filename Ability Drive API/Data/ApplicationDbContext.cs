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
        public DbSet<CreditCard> CreditCards { get; set; }

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
                    IsAvailable = true,
                    PreferredLocations = new List<string> { "Cairo", "Giza", "Alexandria" } // Example locations in Egypt
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
                    IsAvailable = true,
                    PreferredLocations = new List<string> { "6th october", "Helwan", "Nasr City" }
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
                    HasPrioritySeating = true,
                    HasAudioAnnouncements = true
                }
            );
        }
    }
}

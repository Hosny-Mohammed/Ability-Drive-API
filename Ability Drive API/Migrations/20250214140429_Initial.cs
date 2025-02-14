using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ability_Drive_API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ToLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalNormalSeats = table.Column<int>(type: "int", nullable: false),
                    AvailableNormalSeats = table.Column<int>(type: "int", nullable: false),
                    TotalDisabledSeats = table.Column<int>(type: "int", nullable: false),
                    AvailableDisabledSeats = table.Column<int>(type: "int", nullable: false),
                    IsWheelchairAccessible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VehicleRegistration = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    LastKnownLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PreferredLocations = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    PickupLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CancellationReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rides_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rides_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusScheduleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDisabledPassenger = table.Column<bool>(type: "bit", nullable: false),
                    BookingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatBookings_BusSchedules_BusScheduleId",
                        column: x => x.BusScheduleId,
                        principalTable: "BusSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    UsedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVouchers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVouchers_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BusSchedules",
                columns: new[] { "Id", "AvailableDisabledSeats", "AvailableNormalSeats", "BusNumber", "DepartureTime", "FromLocation", "IsWheelchairAccessible", "ToLocation", "TotalDisabledSeats", "TotalNormalSeats" },
                values: new object[,]
                {
                    { 1, 2, 20, "B101", new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), "City Center", true, "North Station", 2, 20 },
                    { 2, 3, 30, "B102", new DateTime(2024, 2, 1, 2, 0, 0, 0, DateTimeKind.Unspecified), "East Station", true, "West Station", 3, 30 },
                    { 3, 2, 25, "B103", new DateTime(2024, 3, 1, 3, 0, 0, 0, DateTimeKind.Unspecified), "South Station", true, "Central Station", 2, 25 }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "IsAvailable", "LastKnownLocation", "LicenseNumber", "Name", "Password", "PhoneNumber", "PreferredLocations", "Rating", "VehicleRegistration", "VehicleType" },
                values: new object[,]
                {
                    { 1, true, null, "DRV12345", "Alice Smith", "pass#123", "01134896510", "[\"Cairo\",\"Giza\",\"Alexandria\"]", 4.8m, "ABC123", "Sedan" },
                    { 2, true, null, "DRV12345", "Ali Mohammed", "pass#123", "01111111111", "[\"6th October\",\"Helwan\",\"Nasr City\"]", 4.8m, "ABC123", "Sedan" },
                    { 3, true, null, "DRV67890", "Sarah Johnson", "pass#789", "0222333444", "[\"Maadi\",\"Zamalek\",\"New Cairo\"]", 4.9m, "XYZ789", "SUV" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsDisabled", "LastName", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@gmail.com", "John", false, "Doe", "password123", "1234567890" },
                    { 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@gmail.com", "Jane", true, "Smith", "password321", "0987654321" },
                    { 3, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.johnson@gmail.com", "Bob", false, "Johnson", "password456", "1122334455" }
                });

            migrationBuilder.InsertData(
                table: "Rides",
                columns: new[] { "Id", "CancellationReason", "Cost", "Destination", "DriverId", "PickupLocation", "RequestTime", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, null, 100m, "Giza", 1, "Cairo", new DateTime(2024, 1, 1, 2, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 1 },
                    { 2, null, 150m, "Nasr City", 2, "Helwan", new DateTime(2024, 2, 1, 3, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 2 },
                    { 3, null, 200m, "New Cairo", 3, "Maadi", new DateTime(2024, 3, 1, 4, 0, 0, 0, DateTimeKind.Unspecified), "Ongoing", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rides_DriverId",
                table: "Rides",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_UserId",
                table: "Rides",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatBookings_BusScheduleId",
                table: "SeatBookings",
                column: "BusScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatBookings_UserId",
                table: "SeatBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserVouchers_UserId",
                table: "UserVouchers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVouchers_VoucherId",
                table: "UserVouchers",
                column: "VoucherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropTable(
                name: "SeatBookings");

            migrationBuilder.DropTable(
                name: "UserVouchers");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "BusSchedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vouchers");
        }
    }
}

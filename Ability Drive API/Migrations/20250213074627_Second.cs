using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ability_Drive_API.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferredLocations",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PreferredLocations",
                value: "[\"Cairo\",\"Giza\",\"Alexandria\"]");

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "IsAvailable", "LastKnownLocation", "LicenseNumber", "Name", "Password", "PhoneNumber", "PreferredLocations", "Rating", "VehicleRegistration", "VehicleType" },
                values: new object[] { 2, true, null, "DRV12345", "Ali Mohammed", "pass#123", "01111111111", "[\"6th october\",\"Helwan\",\"Nasr City\"]", 4.8m, "ABC123", "Sedan" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "PreferredLocations",
                table: "Drivers");
        }
    }
}

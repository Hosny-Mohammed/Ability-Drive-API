using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ability_Drive_API.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "BusSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "BusSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 50);

            migrationBuilder.UpdateData(
                table: "BusSchedules",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 50);

            migrationBuilder.UpdateData(
                table: "BusSchedules",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "BusSchedules");
        }
    }
}

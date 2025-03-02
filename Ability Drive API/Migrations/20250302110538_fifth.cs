using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ability_Drive_API.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "BusSchedules",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "BusSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 50.0);

            migrationBuilder.UpdateData(
                table: "BusSchedules",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 50.0);

            migrationBuilder.UpdateData(
                table: "BusSchedules",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 50.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "BusSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

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
    }
}

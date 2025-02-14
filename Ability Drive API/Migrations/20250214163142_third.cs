using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ability_Drive_API.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SeatBookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SeatBookings",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);
        }
    }
}

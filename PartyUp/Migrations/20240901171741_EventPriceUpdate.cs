using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyUp.Migrations
{
    /// <inheritdoc />
    public partial class EventPriceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventTax",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventTax",
                table: "Events");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Events",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

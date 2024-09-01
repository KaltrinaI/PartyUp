using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyUp.Migrations
{
    /// <inheritdoc />
    public partial class CommentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Comments",
                newName: "TimeOfComment");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "Text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeOfComment",
                table: "Comments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Comments",
                newName: "Content");
        }
    }
}

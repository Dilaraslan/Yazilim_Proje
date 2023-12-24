using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddLibraryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "LibraryModel");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "LibraryModel",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "LibraryModel",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "LibraryModel");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "LibraryModel");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "LibraryModel",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

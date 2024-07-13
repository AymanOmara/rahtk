using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rahtk.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCategoryAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Categories");
        }
    }
}

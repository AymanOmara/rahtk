using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rahtk.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToDrug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Drugs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Drugs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Drugs");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rahtk.Api.Migrations
{
    /// <inheritdoc />
    public partial class DropUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_RahtkUserId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_RahtkUserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "RahtkUserId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RahtkUserId1",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RahtkUserId1",
                table: "Addresses",
                column: "RahtkUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_RahtkUserId1",
                table: "Addresses",
                column: "RahtkUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_RahtkUserId1",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_RahtkUserId1",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "RahtkUserId1",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "RahtkUserId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RahtkUserId",
                table: "Addresses",
                column: "RahtkUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_RahtkUserId",
                table: "Addresses",
                column: "RahtkUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

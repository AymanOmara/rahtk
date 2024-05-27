using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rahtk.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIdIFavoritesProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProductUser_AspNetUsers_UserId1",
                table: "FavoriteProductUser");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteProductUser_UserId1",
                table: "FavoriteProductUser");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "FavoriteProductUser");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FavoriteProductUser",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProductUser_UserId",
                table: "FavoriteProductUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProductUser_AspNetUsers_UserId",
                table: "FavoriteProductUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProductUser_AspNetUsers_UserId",
                table: "FavoriteProductUser");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteProductUser_UserId",
                table: "FavoriteProductUser");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "FavoriteProductUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "FavoriteProductUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProductUser_UserId1",
                table: "FavoriteProductUser",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProductUser_AspNetUsers_UserId1",
                table: "FavoriteProductUser",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rahtk.Api.Migrations
{
    /// <inheritdoc />
    public partial class NotificationsBody : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Notifications");
        }
    }
}

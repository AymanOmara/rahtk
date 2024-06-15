using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rahtk.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationShipProductAndReminders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Reminders_ReminderEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ReminderEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReminderEntityId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductEntityReminderEntity",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    RemindersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntityReminderEntity", x => new { x.ProductsId, x.RemindersId });
                    table.ForeignKey(
                        name: "FK_ProductEntityReminderEntity_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductEntityReminderEntity_Reminders_RemindersId",
                        column: x => x.RemindersId,
                        principalTable: "Reminders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntityReminderEntity_RemindersId",
                table: "ProductEntityReminderEntity",
                column: "RemindersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEntityReminderEntity");

            migrationBuilder.AddColumn<int>(
                name: "ReminderEntityId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ReminderEntityId",
                table: "Products",
                column: "ReminderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Reminders_ReminderEntityId",
                table: "Products",
                column: "ReminderEntityId",
                principalTable: "Reminders",
                principalColumn: "Id");
        }
    }
}

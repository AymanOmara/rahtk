using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rahtk.Api.Migrations
{
    /// <inheritdoc />
    public partial class ProductReminder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEntityReminderEntity");

            migrationBuilder.CreateTable(
                name: "ProductReminder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ReminderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReminder_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReminder_Reminders_ReminderId",
                        column: x => x.ReminderId,
                        principalTable: "Reminders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReminder_ProductId",
                table: "ProductReminder",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReminder_ReminderId",
                table: "ProductReminder",
                column: "ReminderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductReminder");

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
    }
}

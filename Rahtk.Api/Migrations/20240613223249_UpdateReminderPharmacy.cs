using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rahtk.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReminderPharmacy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Drugs_DrugId",
                table: "Reminders");

            migrationBuilder.DropTable(
                name: "OrderDrugItemEntity");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_DrugId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "DrugId",
                table: "Reminders");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Reminders_ReminderEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ReminderEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "ReminderEntityId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "DrugId",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDrugItemEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugId = table.Column<int>(type: "int", nullable: true),
                    DrugCounter = table.Column<int>(type: "int", nullable: false),
                    OrderEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDrugItemEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDrugItemEntity_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDrugItemEntity_Orders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_DrugId",
                table: "Reminders",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDrugItemEntity_DrugId",
                table: "OrderDrugItemEntity",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDrugItemEntity_OrderEntityId",
                table: "OrderDrugItemEntity",
                column: "OrderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Drugs_DrugId",
                table: "Reminders",
                column: "DrugId",
                principalTable: "Drugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

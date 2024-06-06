using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rahtk.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugsToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_OrderDrugItemEntity_DrugId",
                table: "OrderDrugItemEntity",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDrugItemEntity_OrderEntityId",
                table: "OrderDrugItemEntity",
                column: "OrderEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDrugItemEntity");
        }
    }
}

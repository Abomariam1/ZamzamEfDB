using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamzam.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddRePurchProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "ReturnPurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnPurchaseOrders_PurchaseId",
                table: "ReturnPurchaseOrders",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnPurchaseOrders_PurchaseOrders_PurchaseId",
                table: "ReturnPurchaseOrders",
                column: "PurchaseId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnPurchaseOrders_PurchaseOrders_PurchaseId",
                table: "ReturnPurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_ReturnPurchaseOrders_PurchaseId",
                table: "ReturnPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "ReturnPurchaseOrders");
        }
    }
}

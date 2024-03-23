using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamzam.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddRePurchProp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceNumber",
                table: "ReturnPurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "ReturnPurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderType",
                table: "ItemOperation",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnPurchaseOrders_SupplierId",
                table: "ReturnPurchaseOrders",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnPurchaseOrders_Suppliers_SupplierId",
                table: "ReturnPurchaseOrders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnPurchaseOrders_Suppliers_SupplierId",
                table: "ReturnPurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_ReturnPurchaseOrders_SupplierId",
                table: "ReturnPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "ReturnPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "ReturnPurchaseOrders");

            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "ItemOperation");
        }
    }
}

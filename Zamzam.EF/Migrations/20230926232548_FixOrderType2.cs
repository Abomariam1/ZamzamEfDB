using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamzam.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderType2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installment_InstallmentedSaleOrder_OrderId",
                table: "Installment");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentedSaleOrder_Customers_CustomerId",
                table: "InstallmentedSaleOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentedSaleOrder_Orders_Id",
                table: "InstallmentedSaleOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstallmentedSaleOrder",
                table: "InstallmentedSaleOrder");

            migrationBuilder.RenameTable(
                name: "InstallmentedSaleOrder",
                newName: "InstallmentedSaleOrders");

            migrationBuilder.RenameIndex(
                name: "IX_InstallmentedSaleOrder_CustomerId",
                table: "InstallmentedSaleOrders",
                newName: "IX_InstallmentedSaleOrders_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstallmentedSaleOrders",
                table: "InstallmentedSaleOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installment_InstallmentedSaleOrders_OrderId",
                table: "Installment",
                column: "OrderId",
                principalTable: "InstallmentedSaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentedSaleOrders_Customers_CustomerId",
                table: "InstallmentedSaleOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentedSaleOrders_Orders_Id",
                table: "InstallmentedSaleOrders",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installment_InstallmentedSaleOrders_OrderId",
                table: "Installment");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentedSaleOrders_Customers_CustomerId",
                table: "InstallmentedSaleOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentedSaleOrders_Orders_Id",
                table: "InstallmentedSaleOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstallmentedSaleOrders",
                table: "InstallmentedSaleOrders");

            migrationBuilder.RenameTable(
                name: "InstallmentedSaleOrders",
                newName: "InstallmentedSaleOrder");

            migrationBuilder.RenameIndex(
                name: "IX_InstallmentedSaleOrders_CustomerId",
                table: "InstallmentedSaleOrder",
                newName: "IX_InstallmentedSaleOrder_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstallmentedSaleOrder",
                table: "InstallmentedSaleOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installment_InstallmentedSaleOrder_OrderId",
                table: "Installment",
                column: "OrderId",
                principalTable: "InstallmentedSaleOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentedSaleOrder_Customers_CustomerId",
                table: "InstallmentedSaleOrder",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentedSaleOrder_Orders_Id",
                table: "InstallmentedSaleOrder",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

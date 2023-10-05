using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamzam.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddInstallments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Installments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Installments_CustomerId",
                table: "Installments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_OrderId",
                table: "Installments",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_Customers_CustomerId",
                table: "Installments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_InstallmentSaleOrders_OrderId",
                table: "Installments",
                column: "OrderId",
                principalTable: "InstallmentSaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Customers_CustomerId",
                table: "Installments");

            migrationBuilder.DropForeignKey(
                name: "FK_Installments_InstallmentSaleOrders_OrderId",
                table: "Installments");

            migrationBuilder.DropIndex(
                name: "IX_Installments_CustomerId",
                table: "Installments");

            migrationBuilder.DropIndex(
                name: "IX_Installments_OrderId",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Installments");
        }
    }
}

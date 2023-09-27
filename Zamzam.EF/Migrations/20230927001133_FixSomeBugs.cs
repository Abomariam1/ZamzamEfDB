using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamzam.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixSomeBugs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installment_Employees_EmployeeId",
                table: "Installment");

            migrationBuilder.DropForeignKey(
                name: "FK_Installment_InstallmentedSaleOrders_OrderId",
                table: "Installment");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentedSaleOrders_Customers_CustomerId",
                table: "InstallmentedSaleOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentedSaleOrders_Orders_Id",
                table: "InstallmentedSaleOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenance_Orders_Id",
                table: "Maintenance");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenance_SaleOrders_SaleOrderId",
                table: "Maintenance");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Items_ItemId",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Orders_OrderId",
                table: "OrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maintenance",
                table: "Maintenance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstallmentedSaleOrders",
                table: "InstallmentedSaleOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Installment",
                table: "Installment");

            migrationBuilder.RenameTable(
                name: "OrderDetail",
                newName: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "Maintenance",
                newName: "Maintenances");

            migrationBuilder.RenameTable(
                name: "InstallmentedSaleOrders",
                newName: "InstallmentSaleOrders");

            migrationBuilder.RenameTable(
                name: "Installment",
                newName: "Installments");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_ItemId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenance_SaleOrderId",
                table: "Maintenances",
                newName: "IX_Maintenances_SaleOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_InstallmentedSaleOrders_CustomerId",
                table: "InstallmentSaleOrders",
                newName: "IX_InstallmentSaleOrders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Installment_OrderId",
                table: "Installments",
                newName: "IX_Installments_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Installment_EmployeeId",
                table: "Installments",
                newName: "IX_Installments_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maintenances",
                table: "Maintenances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstallmentSaleOrders",
                table: "InstallmentSaleOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Installments",
                table: "Installments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_Employees_EmployeeId",
                table: "Installments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_InstallmentSaleOrders_OrderId",
                table: "Installments",
                column: "OrderId",
                principalTable: "InstallmentSaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentSaleOrders_Customers_CustomerId",
                table: "InstallmentSaleOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentSaleOrders_Orders_Id",
                table: "InstallmentSaleOrders",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Orders_Id",
                table: "Maintenances",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_SaleOrders_SaleOrderId",
                table: "Maintenances",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Items_ItemId",
                table: "OrderDetails",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Employees_EmployeeId",
                table: "Installments");

            migrationBuilder.DropForeignKey(
                name: "FK_Installments_InstallmentSaleOrders_OrderId",
                table: "Installments");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentSaleOrders_Customers_CustomerId",
                table: "InstallmentSaleOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentSaleOrders_Orders_Id",
                table: "InstallmentSaleOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Orders_Id",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_SaleOrders_SaleOrderId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Items_ItemId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maintenances",
                table: "Maintenances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstallmentSaleOrders",
                table: "InstallmentSaleOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Installments",
                table: "Installments");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "OrderDetail");

            migrationBuilder.RenameTable(
                name: "Maintenances",
                newName: "Maintenance");

            migrationBuilder.RenameTable(
                name: "InstallmentSaleOrders",
                newName: "InstallmentedSaleOrders");

            migrationBuilder.RenameTable(
                name: "Installments",
                newName: "Installment");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ItemId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenances_SaleOrderId",
                table: "Maintenance",
                newName: "IX_Maintenance_SaleOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_InstallmentSaleOrders_CustomerId",
                table: "InstallmentedSaleOrders",
                newName: "IX_InstallmentedSaleOrders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Installments_OrderId",
                table: "Installment",
                newName: "IX_Installment_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Installments_EmployeeId",
                table: "Installment",
                newName: "IX_Installment_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maintenance",
                table: "Maintenance",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstallmentedSaleOrders",
                table: "InstallmentedSaleOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Installment",
                table: "Installment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installment_Employees_EmployeeId",
                table: "Installment",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenance_Orders_Id",
                table: "Maintenance",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenance_SaleOrders_SaleOrderId",
                table: "Maintenance",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Items_ItemId",
                table: "OrderDetail",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Orders_OrderId",
                table: "OrderDetail",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

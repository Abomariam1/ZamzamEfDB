using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamzam.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectorToArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Areas_EmployeeId",
                table: "Areas",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Employees_EmployeeId",
                table: "Areas",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Employees_EmployeeId",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Areas_EmployeeId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Areas");
        }
    }
}

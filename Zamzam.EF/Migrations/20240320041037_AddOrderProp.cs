using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamzam.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPayed",
                table: "Orders",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalRemained",
                table: "Orders",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPayed",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalRemained",
                table: "Orders");
        }
    }
}

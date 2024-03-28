using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamzam.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixItemOperationsKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemOperation",
                table: "ItemOperation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemOperation",
                table: "ItemOperation",
                columns: new[] { "OrderId", "ItemId", "OperationType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemOperation",
                table: "ItemOperation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemOperation",
                table: "ItemOperation",
                columns: new[] { "OrderId", "ItemId" });
        }
    }
}

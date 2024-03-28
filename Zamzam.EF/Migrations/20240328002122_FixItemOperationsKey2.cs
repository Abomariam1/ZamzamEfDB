using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamzam.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixItemOperationsKey2: Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemOperation",
                table: "ItemOperation");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItemOperation"
                );

            migrationBuilder.AddColumn<int>(
                name: "Id",
                type: "int",
                table: "ItemOperation",
                nullable: false
                )
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemOperation",
                table: "ItemOperation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOperation_OrderId",
                table: "ItemOperation",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemOperation",
                table: "ItemOperation");

            migrationBuilder.DropIndex(
                name: "IX_ItemOperation_OrderId",
                table: "ItemOperation");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ItemOperation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemOperation",
                table: "ItemOperation",
                columns: new[] { "OrderId", "ItemId", "OperationType" });
        }
    }
}

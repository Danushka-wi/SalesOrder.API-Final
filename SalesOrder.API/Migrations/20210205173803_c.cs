using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesOrder.API.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderLines");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderLines",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderLines");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderLines",
                type: "decimal(18,2)",
                maxLength: 10,
                nullable: false,
                defaultValue: 0m);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePurchaseModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Purchases",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseNo",
                table: "Purchases",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "Purchases",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "Purchases",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PurchaseNo",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Purchases");
        }
    }
}

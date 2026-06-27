using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleFinancialFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CashReceived",
                table: "Sales",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChangeReturn",
                table: "Sales",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Sales",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "Sales",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "Sales",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "Sales",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashReceived",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ChangeReturn",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Sales");
        }
    }
}

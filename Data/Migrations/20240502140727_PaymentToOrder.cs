using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Migrations
{
    /// <inheritdoc />
    public partial class PaymentToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "Payment",
                newName: "OrderID");

            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "Cart",
                newName: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Payment",
                newName: "PaymentID");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Cart",
                newName: "PaymentID");
        }
    }
}

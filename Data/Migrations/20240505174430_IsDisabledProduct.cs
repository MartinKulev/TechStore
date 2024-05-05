using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Migrations
{
    /// <inheritdoc />
    public partial class IsDisabledProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDisabled",
                table: "Product",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDisabled",
                table: "Product");
        }
    }
}

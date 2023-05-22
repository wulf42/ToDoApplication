using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "ShoppingProducts",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ShoppingProducts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ShoppingProducts",
                newName: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ShoppingProducts",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ShoppingProducts",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ShoppingProducts",
                newName: "productId");
        }
    }
}

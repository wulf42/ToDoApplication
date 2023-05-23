using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApplication.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // Nazwa typu zawiera tylko małe litery ascii. Takie nazwy mogą zostać zarezerwowane dla języka.
    public partial class shoppingproduct : Migration
#pragma warning restore CS8981 // Nazwa typu zawiera tylko małe litery ascii. Takie nazwy mogą zostać zarezerwowane dla języka.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingProduct_TasksToDo_TaskToDoId",
                table: "ShoppingProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingProduct",
                table: "ShoppingProduct");

            migrationBuilder.RenameTable(
                name: "ShoppingProduct",
                newName: "ShoppingProducts");

#pragma warning disable RCS1205 // Order named arguments according to the order of parameters.
            migrationBuilder.RenameIndex(
                name: "IX_ShoppingProduct_TaskToDoId",
                table: "ShoppingProducts",
                newName: "IX_ShoppingProducts_TaskToDoId");
#pragma warning restore RCS1205 // Order named arguments according to the order of parameters.

            migrationBuilder.AlterColumn<int>(
                name: "TaskToDoId",
                table: "ShoppingProducts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingProducts",
                table: "ShoppingProducts",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingProducts_TasksToDo_TaskToDoId",
                table: "ShoppingProducts",
                column: "TaskToDoId",
                principalTable: "TasksToDo",
                principalColumn: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingProducts_TasksToDo_TaskToDoId",
                table: "ShoppingProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingProducts",
                table: "ShoppingProducts");

            migrationBuilder.RenameTable(
                name: "ShoppingProducts",
                newName: "ShoppingProduct");

#pragma warning disable RCS1205 // Order named arguments according to the order of parameters.
            migrationBuilder.RenameIndex(
                name: "IX_ShoppingProducts_TaskToDoId",
                table: "ShoppingProduct",
                newName: "IX_ShoppingProduct_TaskToDoId");
#pragma warning restore RCS1205 // Order named arguments according to the order of parameters.

            migrationBuilder.AlterColumn<int>(
                name: "TaskToDoId",
                table: "ShoppingProduct",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingProduct",
                table: "ShoppingProduct",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingProduct_TasksToDo_TaskToDoId",
                table: "ShoppingProduct",
                column: "TaskToDoId",
                principalTable: "TasksToDo",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApplication.Migrations
{
    /// <inheritdoc />
    public partial class dailyTaskModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingProducts_TasksToDo_TaskToDoId",
                table: "ShoppingProducts");

            migrationBuilder.AddColumn<DateOnly>(
                name: "LastDone",
                table: "TasksToDo",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskToDoId",
                table: "ShoppingProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingProducts_TasksToDo_TaskToDoId",
                table: "ShoppingProducts",
                column: "TaskToDoId",
                principalTable: "TasksToDo",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingProducts_TasksToDo_TaskToDoId",
                table: "ShoppingProducts");

            migrationBuilder.DropColumn(
                name: "LastDone",
                table: "TasksToDo");

            migrationBuilder.AlterColumn<int>(
                name: "TaskToDoId",
                table: "ShoppingProducts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingProducts_TasksToDo_TaskToDoId",
                table: "ShoppingProducts",
                column: "TaskToDoId",
                principalTable: "TasksToDo",
                principalColumn: "TaskId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApplication.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // Nazwa typu zawiera tylko małe litery ascii. Takie nazwy mogą zostać zarezerwowane dla języka.

    public partial class tasktodonameschanged : Migration
#pragma warning restore CS8981 // Nazwa typu zawiera tylko małe litery ascii. Takie nazwy mogą zostać zarezerwowane dla języka.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "addedBy",
                table: "TasksToDo",
                newName: "AddedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddedBy",
                table: "TasksToDo",
                newName: "addedBy");
        }
    }
}
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoAplication.Migrations
{
    public partial class AddIsComplitedToDoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplited",
                table: "ToDoItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplited",
                table: "ToDoItems");
        }
    }
}

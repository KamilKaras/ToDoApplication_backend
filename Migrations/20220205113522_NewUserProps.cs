using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoAplication.Migrations
{
    public partial class NewUserProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedTime",
                table: "AspNetUsers",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "AspNetUsers",
                newName: "Created");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "AspNetUsers",
                newName: "ModifiedTime");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "AspNetUsers",
                newName: "CreatedTime");
        }
    }
}

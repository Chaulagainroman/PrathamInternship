using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Employee",
                schema: "public",
                newName: "Employee");

            migrationBuilder.AddColumn<long>(
                name: "Phone",
                table: "Employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Post",
                table: "Employee",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Post",
                table: "Employee");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employee",
                newSchema: "public");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationBooks.Migrations
{
    public partial class AccountUserToDatabase3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessagerUsername",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessagerUsername",
                table: "AspNetUsers");
        }
    }
}

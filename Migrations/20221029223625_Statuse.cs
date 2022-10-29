using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ski_Service_Management.Migrations
{
    public partial class Statuse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Status",
                newName: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Status",
                newName: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ski_Service_Management.Migrations
{
    public partial class Statusee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Status_StatusId",
                table: "Registrations");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Registrations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Status_StatusId",
                table: "Registrations",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Status_StatusId",
                table: "Registrations");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Status_StatusId",
                table: "Registrations",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

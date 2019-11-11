using Microsoft.EntityFrameworkCore.Migrations;

namespace LogCenter.Infra.Migrations
{
    public partial class AddLogUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Logs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Logs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}

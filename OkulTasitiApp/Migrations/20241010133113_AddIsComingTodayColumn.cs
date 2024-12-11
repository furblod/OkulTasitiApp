using Microsoft.EntityFrameworkCore.Migrations;

namespace OkulTasitiApp.Migrations
{
    public partial class AddIsComingTodayColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComingToday",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComingToday",
                table: "Students");
        }
    }
}

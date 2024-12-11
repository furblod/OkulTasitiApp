using Microsoft.EntityFrameworkCore.Migrations;

namespace OkulTasitiApp.Migrations
{
    public partial class AddDriverIDToStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverID",
                table: "Students",
                type: "nvarchar(450)",  // Eğer AspNetUsers tablosu ile ilişkilendiriyorsanız nvarchar(450) kullanmanız daha uygun olur
                nullable: true);

            // Eğer School tablosuna da eklediyseniz:
            migrationBuilder.AddColumn<string>(
                name: "DriverID",
                table: "School",
                type: "nvarchar(450)",
                nullable: true);

            // Foreign key ve index oluşturma:
            migrationBuilder.CreateIndex(
                name: "IX_School_DriverID",
                table: "School",
                column: "DriverID");

            migrationBuilder.AddForeignKey(
                name: "FK_School_AspNetUsers_DriverID",
                table: "School",
                column: "DriverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_AspNetUsers_DriverID",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_School_DriverID",
                table: "School");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "School");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "Students");
        }
    }

}

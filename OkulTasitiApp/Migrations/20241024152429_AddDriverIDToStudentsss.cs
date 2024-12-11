using Microsoft.EntityFrameworkCore.Migrations;

namespace OkulTasitiApp.Migrations
{
    public partial class AddDriverIDToStudentsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_AspNetUsers_DriverID",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolDriver_AspNetUsers_DriverID",
                table: "SchoolDriver");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolDriver_School_SchoolID",
                table: "SchoolDriver");

            migrationBuilder.DropIndex(
                name: "IX_School_DriverID",
                table: "School");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolDriver",
                table: "SchoolDriver");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "School");

            migrationBuilder.RenameTable(
                name: "SchoolDriver",
                newName: "SchoolDrivers");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolDriver_DriverID",
                table: "SchoolDrivers",
                newName: "IX_SchoolDrivers_DriverID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolDrivers",
                table: "SchoolDrivers",
                columns: new[] { "SchoolID", "DriverID" });

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolDrivers_AspNetUsers_DriverID",
                table: "SchoolDrivers",
                column: "DriverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolDrivers_School_SchoolID",
                table: "SchoolDrivers",
                column: "SchoolID",
                principalTable: "School",
                principalColumn: "SchoolID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolDrivers_AspNetUsers_DriverID",
                table: "SchoolDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolDrivers_School_SchoolID",
                table: "SchoolDrivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolDrivers",
                table: "SchoolDrivers");

            migrationBuilder.RenameTable(
                name: "SchoolDrivers",
                newName: "SchoolDriver");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolDrivers_DriverID",
                table: "SchoolDriver",
                newName: "IX_SchoolDriver_DriverID");

            migrationBuilder.AddColumn<string>(
                name: "DriverID",
                table: "School",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolDriver",
                table: "SchoolDriver",
                columns: new[] { "SchoolID", "DriverID" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolDriver_AspNetUsers_DriverID",
                table: "SchoolDriver",
                column: "DriverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolDriver_School_SchoolID",
                table: "SchoolDriver",
                column: "SchoolID",
                principalTable: "School",
                principalColumn: "SchoolID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

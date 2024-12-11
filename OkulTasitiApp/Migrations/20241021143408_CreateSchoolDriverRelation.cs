using Microsoft.EntityFrameworkCore.Migrations;

namespace OkulTasitiApp.Migrations
{
    public partial class CreateSchoolDriverRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchoolDriver",
                columns: table => new
                {
                    SchoolID = table.Column<int>(type: "int", nullable: false),
                    DriverID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolDriver", x => new { x.SchoolID, x.DriverID });
                    table.ForeignKey(
                        name: "FK_SchoolDriver_AspNetUsers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolDriver_School_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolDriver_DriverID",
                table: "SchoolDriver",
                column: "DriverID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolDriver");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace FPSOManagerApi_CS.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vessels",
                columns: table => new
                {
                    code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vessels", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "equipment",
                columns: table => new
                {
                    code = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    Vesselcode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment", x => x.code);
                    table.ForeignKey(
                        name: "FK_equipment_vessels_Vesselcode",
                        column: x => x.Vesselcode,
                        principalTable: "vessels",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_equipment_Vesselcode",
                table: "equipment",
                column: "Vesselcode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "equipment");

            migrationBuilder.DropTable(
                name: "vessels");
        }
    }
}

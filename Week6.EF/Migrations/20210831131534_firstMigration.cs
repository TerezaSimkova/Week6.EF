using Microsoft.EntityFrameworkCore.Migrations;

namespace Week6.EF.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    BattleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.BattleId);
                });

            migrationBuilder.CreateTable(
                name: "Knights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BattleKnight",
                columns: table => new
                {
                    BattlesBattleId = table.Column<int>(type: "int", nullable: false),
                    KnightsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleKnight", x => new { x.BattlesBattleId, x.KnightsId });
                    table.ForeignKey(
                        name: "FK_BattleKnight_Battles_BattlesBattleId",
                        column: x => x.BattlesBattleId,
                        principalTable: "Battles",
                        principalColumn: "BattleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BattleKnight_Knights_KnightsId",
                        column: x => x.KnightsId,
                        principalTable: "Knights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_Knights_KnightId",
                        column: x => x.KnightId,
                        principalTable: "Knights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BattleKnight_KnightsId",
                table: "BattleKnight",
                column: "KnightsId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_KnightId",
                table: "Weapons",
                column: "KnightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleKnight");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DropTable(
                name: "Knights");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Week6.EF.Migrations
{
    public partial class HorseKnight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Horse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horse_Knights_KnightId",
                        column: x => x.KnightId,
                        principalTable: "Knights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horse_KnightId",
                table: "Horse",
                column: "KnightId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Horse");
        }
    }
}

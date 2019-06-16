using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityConfiguration.Migrations
{
    public partial class AddPathcolumntoImagetoGameentityv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_Path",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Games_Path",
                table: "Games",
                column: "Path",
                unique: true);
        }
    }
}

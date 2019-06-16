using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityConfiguration.Migrations
{
    public partial class AddPathcolumntoImagetoGameentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Games",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Path",
                table: "Games",
                column: "Path",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_Path",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Games");
        }
    }
}

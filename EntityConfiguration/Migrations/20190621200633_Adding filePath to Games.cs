using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityConfiguration.Migrations
{
    public partial class AddingfilePathtoGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "Games");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityConfiguration.Migrations
{
    public partial class UniquefieldsaddedforDevelopersNameDevelopersWebsite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Developers_Name",
                table: "Developers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Developers_Website",
                table: "Developers",
                column: "Website",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Developers_Name",
                table: "Developers");

            migrationBuilder.DropIndex(
                name: "IX_Developers_Website",
                table: "Developers");
        }
    }
}

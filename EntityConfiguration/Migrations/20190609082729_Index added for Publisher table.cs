using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityConfiguration.Migrations
{
    public partial class IndexaddedforPublishertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Publishers_ISIN",
                table: "Publishers",
                column: "ISIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Name",
                table: "Publishers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Website",
                table: "Publishers",
                column: "Website",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Publishers_ISIN",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_Name",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_Website",
                table: "Publishers");
        }
    }
}

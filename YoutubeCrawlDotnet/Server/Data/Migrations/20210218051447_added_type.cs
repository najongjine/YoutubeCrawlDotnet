using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeCrawlDotnet.Server.Data.Migrations
{
    public partial class added_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "OtherCrawledVideos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "OtherCrawledVideos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OtherCrawledVideos_VideoUrl",
                table: "OtherCrawledVideos",
                column: "VideoUrl",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OtherCrawledVideos_VideoUrl",
                table: "OtherCrawledVideos");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "OtherCrawledVideos");

            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "OtherCrawledVideos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

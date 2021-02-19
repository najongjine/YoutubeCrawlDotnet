using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeCrawlDotnet.Server.Data.Migrations
{
    public partial class other_video_added_thumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "OtherCrawledVideos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "OtherCrawledVideos");
        }
    }
}

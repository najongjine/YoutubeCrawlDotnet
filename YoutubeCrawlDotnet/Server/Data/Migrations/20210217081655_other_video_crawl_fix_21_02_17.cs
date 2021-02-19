using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeCrawlDotnet.Server.Data.Migrations
{
    public partial class other_video_crawl_fix_21_02_17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "OtherCrawledVideos");

            migrationBuilder.RenameColumn(
                name: "ThumbnailDefault",
                table: "OtherCrawledVideos",
                newName: "TagString");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "OtherCrawledVideos",
                newName: "SiteName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "OtherCrawledVideos",
                newName: "Author");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TagString",
                table: "OtherCrawledVideos",
                newName: "ThumbnailDefault");

            migrationBuilder.RenameColumn(
                name: "SiteName",
                table: "OtherCrawledVideos",
                newName: "PublishedAt");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "OtherCrawledVideos",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "ChannelId",
                table: "OtherCrawledVideos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

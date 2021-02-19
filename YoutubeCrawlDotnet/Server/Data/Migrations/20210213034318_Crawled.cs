using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YoutubeCrawlDotnet.Server.Data.Migrations
{
  public partial class Crawled : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Crawled",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            VideoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Order = table.Column<int>(type: "int", nullable: false),
            Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ThumbnailDefault = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ThumbnailMedium = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ThumbnailHigh = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ChannelTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
            PublishedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ChannelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
            FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            FullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Crawled", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "PrivateCrawled",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CrawledId = table.Column<int>(type: "int", nullable: false),
            Order = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_PrivateCrawled", x => x.Id);
            table.ForeignKey(
                      name: "FK_PrivateCrawled_Crawled_CrawledId",
                      column: x => x.CrawledId,
                      principalTable: "Crawled",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "PublicCrawled",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CrawledId = table.Column<int>(type: "int", nullable: false),
            Order = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_PublicCrawled", x => x.Id);
            table.ForeignKey(
                      name: "FK_PublicCrawled_Crawled_CrawledId",
                      column: x => x.CrawledId,
                      principalTable: "Crawled",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_PrivateCrawled_CrawledId",
          table: "PrivateCrawled",
          column: "CrawledId");

      migrationBuilder.CreateIndex(
          name: "IX_PublicCrawled_CrawledId",
          table: "PublicCrawled",
          column: "CrawledId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "PrivateCrawled");

      migrationBuilder.DropTable(
          name: "PublicCrawled");

      migrationBuilder.DropTable(
          name: "Crawled");

      migrationBuilder.CreateTable(
          name: "Test",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Test", x => x.Id);
          });
    }
  }
}

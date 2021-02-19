using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeCrawlDotnet.Server.Data.Migrations
{
    public partial class change_colum_int_to_long : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      migrationBuilder.DropForeignKey(
        name: "FK_PrivateCrawled_Crawled_CrawledId",
        table: "PrivateCrawled"
        );
      migrationBuilder.DropForeignKey(
        name: "FK_PublicCrawled_Crawled_CrawledId",
        table: "PublicCrawled"
        );
      migrationBuilder.DropPrimaryKey(
        name: "PK_PublicCrawled",
        table: "PublicCrawled"
        );
      migrationBuilder.DropPrimaryKey(
        name: "PK_PrivateCrawled",
        table: "PrivateCrawled"
        );
      migrationBuilder.DropPrimaryKey(
        name:"PK_Crawled",
        table:"Crawled"
        );
      
      
      migrationBuilder.AlterColumn<long>(
                name: "Order",
                table: "PublicCrawled",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "CrawledId",
                table: "PublicCrawled",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "PublicCrawled",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Order",
                table: "PrivateCrawled",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "CrawledId",
                table: "PrivateCrawled",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "PrivateCrawled",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "VideoId",
                table: "Crawled",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "Order",
                table: "Crawled",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Crawled",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Crawled_VideoId",
                table: "Crawled",
                column: "VideoId",
                unique: true);
      migrationBuilder.AddPrimaryKey(
            name: "PK_Crawled",
            table: "Crawled",
            column: "Id");
      migrationBuilder.AddPrimaryKey(
            name: "PK_PublicCrawled",
            table: "PublicCrawled",
            column: "Id");
      migrationBuilder.AddPrimaryKey(
            name: "PK_PrivateCrawled",
            table: "PrivateCrawled",
            column: "Id");
      migrationBuilder.AddForeignKey(
            name: "FK_PublicCrawled_Crawled_CrawledId",
            table: "PublicCrawled",
            column: "CrawledId",
            principalTable: "Crawled",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
      migrationBuilder.AddForeignKey(
            name: "FK_PrivateCrawled_Crawled_CrawledId",
            table: "PrivateCrawled",
            column: "CrawledId",
            principalTable: "Crawled",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Crawled_VideoId",
                table: "Crawled");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "PublicCrawled",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CrawledId",
                table: "PublicCrawled",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PublicCrawled",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "PrivateCrawled",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CrawledId",
                table: "PrivateCrawled",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PrivateCrawled",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "VideoId",
                table: "Crawled",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Crawled",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Crawled",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}

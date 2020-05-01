using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStream.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    SongId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongName = table.Column<string>(maxLength: 100, nullable: false),
                    SongFileCover = table.Column<string>(maxLength: 500, nullable: false),
                    SongUrl = table.Column<string>(maxLength: 800, nullable: false),
                    SongDuration = table.Column<string>(maxLength: 10, nullable: false),
                    SingerName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.SongId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}

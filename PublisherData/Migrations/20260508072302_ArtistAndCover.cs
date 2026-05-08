using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    public partial class ArtistAndCover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Covers",
                columns: table => new
                {
                    CoverId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DesignIdeas = table.Column<string>(type: "TEXT", nullable: false),
                    DigitalOnly = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Covers", x => x.CoverId);
                });

            migrationBuilder.CreateTable(
                name: "ArtistCover",
                columns: table => new
                {
                    ArtistsArtistId = table.Column<int>(type: "INTEGER", nullable: false),
                    CoversCoverId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistCover", x => new { x.ArtistsArtistId, x.CoversCoverId });
                    table.ForeignKey(
                        name: "FK_ArtistCover_Artists_ArtistsArtistId",
                        column: x => x.ArtistsArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistCover_Covers_CoversCoverId",
                        column: x => x.CoversCoverId,
                        principalTable: "Covers",
                        principalColumn: "CoverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistCover_CoversCoverId",
                table: "ArtistCover",
                column: "CoversCoverId");
            migrationBuilder.CreateIndex(
                name: "IX_ArtistCover_ArtistsArtistId",
                table: "ArtistCover",
                column: "ArtistsArtistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistCover");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Covers");
        }
    }
}

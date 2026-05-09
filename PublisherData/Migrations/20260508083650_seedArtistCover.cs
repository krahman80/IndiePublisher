using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    public partial class seedArtistCover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistId", "FirstName", "LastName" },
                values: new object[] { 1, "Pablo", "Picasso" });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistId", "FirstName", "LastName" },
                values: new object[] { 2, "Dee", "Bell" });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistId", "FirstName", "LastName" },
                values: new object[] { 3, "Katharine", "Kuharic" });

            migrationBuilder.InsertData(
                table: "Covers",
                columns: new[] { "CoverId", "DesignIdeas", "DigitalOnly" },
                values: new object[] { 1, "How about a left hand in the dark?", false });

            migrationBuilder.InsertData(
                table: "Covers",
                columns: new[] { "CoverId", "DesignIdeas", "DigitalOnly" },
                values: new object[] { 2, "Should we put a clock?", true });

            migrationBuilder.InsertData(
                table: "Covers",
                columns: new[] { "CoverId", "DesignIdeas", "DigitalOnly" },
                values: new object[] { 3, "A big ear in the clouds?", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "ArtistId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "ArtistId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "ArtistId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 3);
        }
    }
}

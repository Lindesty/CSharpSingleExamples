using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniEFCoreWithSqliteTest.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AuthorName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Publisher = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PublicationYearUtcTick = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorName", "BookName", "PublicationYearUtcTick", "Publisher" },
                values: new object[,]
                {
                    { 1L, "authorName1", "bookName1", 638984221279990968L, "publisher1" },
                    { 2L, "authorName2", "bookName2", 638984221279991175L, "publisher2" },
                    { 3L, "authorName3", "bookName3", 638984221279991177L, "publisher3" },
                    { 4L, "authorName4", "bookName4", 638984221279991178L, "publisher4" },
                    { 5L, "authorName5", "bookName5", 638984221279991179L, "publisher5" },
                    { 6L, "authorName6", "bookName6", 638984221279991180L, "publisher6" },
                    { 7L, "authorName7", "bookName7", 638984221279991181L, "publisher7" },
                    { 8L, "authorName8", "bookName8", 638984221279991182L, "publisher8" },
                    { 9L, "authorName9", "bookName9", 638984221279991183L, "publisher9" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}

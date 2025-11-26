using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _4_EFCoreWithSqliteInWPF.Migrations
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
                    { 1L, "authorName1", "bookName1", 0L, "publisher1" },
                    { 2L, "authorName2", "bookName2", 0L, "publisher2" },
                    { 3L, "authorName3", "bookName3", 0L, "publisher3" },
                    { 4L, "authorName4", "bookName4", 0L, "publisher4" },
                    { 5L, "authorName5", "bookName5", 0L, "publisher5" },
                    { 6L, "authorName6", "bookName6", 0L, "publisher6" },
                    { 7L, "authorName7", "bookName7", 0L, "publisher7" },
                    { 8L, "authorName8", "bookName8", 0L, "publisher8" },
                    { 9L, "authorName9", "bookName9", 0L, "publisher9" }
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

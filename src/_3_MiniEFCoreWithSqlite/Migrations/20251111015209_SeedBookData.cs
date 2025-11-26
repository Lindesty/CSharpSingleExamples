using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniEFCoreWithSqliteTest.Migrations
{
    /// <inheritdoc />
    public partial class SeedBookData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1L,
                column: "PublicationYearUtcTick",
                value: 638396640000000000L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2L,
                column: "PublicationYearUtcTick",
                value: 638396640000000000L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3L,
                column: "PublicationYearUtcTick",
                value: 638396640000000000L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4L,
                column: "PublicationYearUtcTick",
                value: 638396640000000000L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5L,
                column: "PublicationYearUtcTick",
                value: 638396640000000000L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6L,
                column: "PublicationYearUtcTick",
                value: 638396640000000000L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7L,
                column: "PublicationYearUtcTick",
                value: 638396640000000000L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8L,
                column: "PublicationYearUtcTick",
                value: 638396640000000000L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9L,
                column: "PublicationYearUtcTick",
                value: 638396640000000000L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1L,
                column: "PublicationYearUtcTick",
                value: 638984221279990968L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2L,
                column: "PublicationYearUtcTick",
                value: 638984221279991175L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3L,
                column: "PublicationYearUtcTick",
                value: 638984221279991177L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4L,
                column: "PublicationYearUtcTick",
                value: 638984221279991178L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5L,
                column: "PublicationYearUtcTick",
                value: 638984221279991179L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6L,
                column: "PublicationYearUtcTick",
                value: 638984221279991180L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7L,
                column: "PublicationYearUtcTick",
                value: 638984221279991181L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8L,
                column: "PublicationYearUtcTick",
                value: 638984221279991182L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9L,
                column: "PublicationYearUtcTick",
                value: 638984221279991183L);
        }
    }
}

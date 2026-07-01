using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Floristo.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class RenamePublishedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unpublished_at",
                schema: "floristo",
                table: "product",
                newName: "published_at");

            migrationBuilder.RenameColumn(
                name: "unpublished_at",
                schema: "floristo",
                table: "category",
                newName: "published_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "published_at",
                schema: "floristo",
                table: "product",
                newName: "unpublished_at");

            migrationBuilder.RenameColumn(
                name: "published_at",
                schema: "floristo",
                table: "category",
                newName: "unpublished_at");
        }
    }
}

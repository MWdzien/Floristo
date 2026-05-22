using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Floristo.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class RenameDeactivatedAtToUnpublishedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deactivated_at",
                schema: "floristo",
                table: "category",
                newName: "unpublished_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unpublished_at",
                schema: "floristo",
                table: "category",
                newName: "deactivated_at");
        }
    }
}

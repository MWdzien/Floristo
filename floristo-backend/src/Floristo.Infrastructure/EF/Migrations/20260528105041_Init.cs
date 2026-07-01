using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Floristo.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "floristo");

            migrationBuilder.CreateTable(
                name: "category",
                schema: "floristo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    seo_description = table.Column<string>(type: "text", nullable: true),
                    seo_title = table.Column<string>(type: "text", nullable: true),
                    slug = table.Column<string>(type: "text", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    unpublished_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                schema: "floristo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deactivated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "floristo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    allow_engraving = table.Column<bool>(type: "boolean", nullable: false),
                    allow_gift_tag = table.Column<bool>(type: "boolean", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    seo_description = table.Column<string>(type: "text", nullable: true),
                    seo_title = table.Column<string>(type: "text", nullable: true),
                    slug = table.Column<string>(type: "text", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    unpublished_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    height = table.Column<decimal>(type: "numeric", nullable: true),
                    width = table.Column<decimal>(type: "numeric", nullable: true),
                    depth = table.Column<decimal>(type: "numeric", nullable: true),
                    diameter = table.Column<decimal>(type: "numeric", nullable: true),
                    length = table.Column<decimal>(type: "numeric", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "floristo",
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            // manually added search_vector for products - it should only be visible by ReadDbContext and migrations are
            // done with WriteDbContext
            migrationBuilder.Sql(@"
                ALTER TABLE floristo.product
                ADD COLUMN search_vector tsvector
                GENERATED ALWAYS AS (
                    setweight(to_tsvector('simple', coalesce(name, '')), 'A') ||
                    setweight(to_tsvector('simple', coalesce(description, '')), 'B')
                    ) STORED;

                CREATE INDEX ix_product_search_vector ON floristo.product USING GIN(search_vector);
            ");

            migrationBuilder.CreateTable(
                name: "product_image",
                schema: "floristo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_image_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "floristo",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_slug",
                schema: "floristo",
                table: "category",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_email",
                schema: "floristo",
                table: "customer",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                schema: "floristo",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_slug",
                schema: "floristo",
                table: "product",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_image_product_id_sort_order",
                schema: "floristo",
                table: "product_image",
                columns: new[] { "product_id", "sort_order" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer",
                schema: "floristo");

            migrationBuilder.DropTable(
                name: "product_image",
                schema: "floristo");

            migrationBuilder.DropTable(
                name: "product",
                schema: "floristo");

            migrationBuilder.DropTable(
                name: "category",
                schema: "floristo");
        }
    }
}

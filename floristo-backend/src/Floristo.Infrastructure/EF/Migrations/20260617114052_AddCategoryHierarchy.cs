using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Floristo.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "parent_id",
                schema: "floristo",
                table: "category",
                type: "uuid",
                nullable: true);
            
            migrationBuilder.Sql(@"
                ALTER TABLE floristo.category
                ADD CONSTRAINT fk_category_parent
                FOREIGN KEY (parent_id) REFERENCES floristo.category(id)
                ON DELETE RESTRICT;
            ");
            
            migrationBuilder.Sql(@"CREATE INDEX ix_category_parent_id
                ON floristo.category(parent_id);
            ");
            
            migrationBuilder.Sql(@"
                ALTER TABLE floristo.category
                ADD CONSTRAINT uq_category_parent_sort_order
                UNIQUE NULLS NOT DISTINCT (parent_id, sort_order);
            ");
        }
        
        

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE floristo.category DROP CONSTRAINT uq_category_parent_sort_order;");
            migrationBuilder.Sql("DROP INDEX floristo.ix_category_parent_id;");
            migrationBuilder.Sql("ALTER TABLE floristo.category DROP CONSTRAINT fk_category_parent;");
            
            migrationBuilder.DropColumn(
                name: "parent_id",
                schema: "floristo",
                table: "category");
        }
    }
}

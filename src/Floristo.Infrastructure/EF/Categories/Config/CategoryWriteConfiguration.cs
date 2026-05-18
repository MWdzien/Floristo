using Floristo.Domain.Categories.Entities;
using Floristo.Domain.Categories.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Floristo.Infrastructure.EF.Categories.Config;

internal sealed class CategoryWriteConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category");
        builder.HasKey(c => c.Id);
        
        var categoryNameConverter = new ValueConverter<CategoryName, string>(cn => cn.Value,
            cn => new CategoryName(cn));
        
        var categoryDescriptionConverter = new ValueConverter<CategoryDescription, string>(cd => cd.Value,
            cd => new CategoryDescription(cd));
        
        var categorySlugConverter = new ValueConverter<CategorySlug, string>(cs => cs.Value,
            cs => new CategorySlug(cs));
        
        var categorySortOrderConverter = new ValueConverter<CategorySortOrder, int>(cso => cso.Value,
            cso => new CategorySortOrder(cso));
        
        var categoryImageUrlConverter = new ValueConverter<CategoryImageUrl, string>(ciu => ciu.Value,
            ciu => new CategoryImageUrl(ciu));
        
        var categorySeoTitleConverter = new ValueConverter<CategorySeoTitle, string>(cst => cst.Value,
            cst => new CategorySeoTitle(cst));
        
        var categorySeoDescriptionConverter = new ValueConverter<CategorySeoDescription, string>(csd => csd.Value,
            csd => new CategorySeoDescription(csd));
        
        var categoryCreatedAtConverter = new ValueConverter<CategoryCreatedAt, DateTime>(cca => cca.Value,
            cca => new CategoryCreatedAt(cca));
        
        var categoryUnpublishedAtConverter = new ValueConverter<CategoryUnpublishedAt, DateTime>(cda => cda.Value,
            cda => new CategoryUnpublishedAt(cda));
        
        builder
            .Property(c => c.Id)
            .HasConversion(id => id.Value, id => new CategoryId(id))
            .HasColumnName("id");
        
        builder
            .Property(c => c.Version)
            .HasColumnName("version")
            .IsConcurrencyToken();
        
        builder
            .Property(typeof(CategoryName), "_name")
            .HasConversion(categoryNameConverter)
            .HasColumnName("name");

        builder
            .Property(typeof(CategoryDescription), "_description")
            .HasConversion(categoryDescriptionConverter)
            .HasColumnName("description")
            .IsRequired(false);
        
        builder
            .Property(typeof(CategorySlug), "_slug")
            .HasConversion(categorySlugConverter)
            .HasColumnName("slug");
        
        builder
            .Property(typeof(CategorySortOrder), "_sortOrder")
            .HasConversion(categorySortOrderConverter)
            .HasColumnName("sort_order");
        
        builder
            .Property(typeof(CategoryImageUrl), "_imageUrl")
            .HasConversion(categoryImageUrlConverter)
            .HasColumnName("image_url")
            .IsRequired(false);
        
        builder
            .Property(typeof(CategorySeoTitle), "_seoTitle")
            .HasConversion(categorySeoTitleConverter)
            .HasColumnName("seo_title")
            .IsRequired(false);
        
        builder
            .Property(typeof(CategorySeoDescription), "_seoDescription")
            .HasConversion(categorySeoDescriptionConverter)
            .HasColumnName("seo_description")
            .IsRequired(false);
        
        builder
            .Property(typeof(CategoryCreatedAt), "_createdAt")
            .HasConversion(categoryCreatedAtConverter)
            .HasColumnName("created_at");
        
        builder
            .Property(typeof(CategoryUnpublishedAt), "_unpublishedAt")
            .HasConversion(categoryUnpublishedAtConverter)
            .HasColumnName("unpublished_at")
            .IsRequired(false);
        
        builder.HasIndex("_slug").IsUnique();
    }
}
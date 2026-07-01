using Floristo.Infrastructure.EF.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Floristo.Infrastructure.EF.Products.Config;

internal sealed class ProductReadConfiguration : IEntityTypeConfiguration<ProductReadModel>, IEntityTypeConfiguration<ProductImageReadModel>
{
    public void Configure(EntityTypeBuilder<ProductReadModel> builder)
    {
        builder.ToTable("product");
        builder.HasKey(p => p.Id);

        builder
            .HasMany(p => p.Images)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId);
        
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Version).HasColumnName("version");
        builder.Property(p => p.Name).HasColumnName("name");
        builder.Property(p => p.Slug).HasColumnName("slug");
        builder.Property(p => p.Description).HasColumnName("description");
        builder.Property(p => p.Price).HasColumnName("price");
        builder.Property(p => p.CategoryId).HasColumnName("category_id");
        builder.Property(p => p.Height).HasColumnName("height");
        builder.Property(p => p.Width).HasColumnName("width");
        builder.Property(p => p.Depth).HasColumnName("depth");
        builder.Property(p => p.Diameter).HasColumnName("diameter");
        builder.Property(p => p.Length).HasColumnName("length");
        builder.Property(p => p.SeoTitle).HasColumnName("seo_title");
        builder.Property(p => p.SeoDescription).HasColumnName("seo_description");
        builder.Property(p => p.AllowEngraving).HasColumnName("allow_engraving");
        builder.Property(p => p.AllowGiftTag).HasColumnName("allow_gift_tag");
        builder.Property(p => p.SortOrder).HasColumnName("sort_order");
        builder.Property(p => p.CreatedAt).HasColumnName("created_at");
        builder.Property(p => p.PublishedAt).HasColumnName("published_at");
        builder.Property(p => p.SearchVector).HasColumnName("search_vector").HasColumnType("tsvector")
            .ValueGeneratedOnAddOrUpdate();
        
        builder.HasIndex(p => p.SearchVector).HasMethod("GIN");
    }

    public void Configure(EntityTypeBuilder<ProductImageReadModel> builder)
    {
        builder.ToTable("product_image");

        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.Id).HasColumnName("id");
        builder.Property(pi => pi.Url).HasColumnName("url");
        builder.Property(pi => pi.SortOrder).HasColumnName("sort_order");
        builder.Property(pi => pi.ProductId).HasColumnName("product_id");
    }
}
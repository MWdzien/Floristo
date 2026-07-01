using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.Entities;
using Floristo.Domain.Products.ValueObjects;
using Floristo.Domain.Products.ValueObjects.ProductImage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Floristo.Infrastructure.EF.Products.Config;

internal sealed class ProductWriteConfiguration : IEntityTypeConfiguration<Product>, IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");
        builder.HasKey(p => p.Id);
        
        var productNameConverter = new ValueConverter<ProductName, string>(pn => pn.Value,
            pn => new(pn));
        var productSlugConverter = new ValueConverter<ProductSlug, string>(ps => ps.Value,
            ps => new(ps));
        var productDescriptionConverter = new ValueConverter<ProductDescription, string>(pd => pd.Value,
            pd => new (pd));
        var productPriceConverter = new ValueConverter<ProductPrice, decimal>(pp => pp.Value,
            pp => new (pp));
        var productCategoryIdConverter = new ValueConverter<CategoryId, Guid>(cid => cid.Value,
            cid => new (cid));
        var productSeoTitleConverter = new ValueConverter<ProductSeoTitle, string>(pst => pst.Value,
            pst => new (pst));
        var productSeoDescriptionConverter = new ValueConverter<ProductSeoDescription, string>(psd => psd.Value,
            psd => new (psd));
        var productSortOrderConverter = new ValueConverter<ProductSortOrder, int>(pso => pso.Value,
            pso => new (pso));
        var productCreatedAtConverter = new ValueConverter<ProductCreatedAt, DateTime>(pca => pca.Value,
            pca => new (pca));
        var productPublishedAtConverter = new ValueConverter<ProductPublishedAt, DateTime>(ppa => ppa.Value,
            ppa => new (ppa));

        builder
            .Property(p => p.Id)
            .HasConversion(id => id.Value, id => new ProductId(id))
            .HasColumnName("id");
        
        builder
            .Property(p => p.Version)
            .HasColumnName("version")
            .IsConcurrencyToken();
        
        builder
            .Property(typeof(ProductName), "_name")
            .HasConversion(productNameConverter)
            .HasColumnName("name");
        
        builder
            .Property(typeof(ProductSlug), "_slug")
            .HasConversion(productSlugConverter)
            .HasColumnName("slug");

        builder.HasIndex("_slug").IsUnique();
        
        builder
            .Property(typeof(ProductDescription), "_description")
            .HasConversion(productDescriptionConverter)
            .HasColumnName("description")
            .IsRequired(false);
        
        builder
            .Property(typeof(ProductPrice), "_price")
            .HasConversion(productPriceConverter)
            .HasColumnName("price");
        
        builder
            .Property(typeof(CategoryId), "_categoryId")
            .HasConversion(productCategoryIdConverter)
            .HasColumnName("category_id");

        builder.OwnsOne(typeof(ProductDimensions), "_productDimensions", d =>
        {
            d.Property("Height").HasColumnName("height").IsRequired(false);
            d.Property("Width").HasColumnName("width").IsRequired(false);
            d.Property("Depth").HasColumnName("depth").IsRequired(false);
            d.Property("Diameter").HasColumnName("diameter").IsRequired(false);
            d.Property("Length").HasColumnName("length").IsRequired(false);
        });
        
        builder
            .Property(typeof(ProductSeoTitle), "_seoTitle")
            .HasConversion(productSeoTitleConverter)
            .HasColumnName("seo_title")
            .IsRequired(false);
        
        builder
            .Property(typeof(ProductSeoDescription), "_seoDescription")
            .HasConversion(productSeoDescriptionConverter)
            .HasColumnName("seo_description")
            .IsRequired(false);
        
        builder
            .Property(typeof(bool), "_allowEngraving")
            .HasColumnName("allow_engraving");
        
        builder
            .Property(typeof(bool), "_allowGiftTag")
            .HasColumnName("allow_gift_tag");
        
        builder
            .Property(typeof(ProductSortOrder), "_sortOrder")
            .HasConversion(productSortOrderConverter)
            .HasColumnName("sort_order");
        
        builder
            .Property(typeof(ProductCreatedAt), "_createdAt")
            .HasConversion(productCreatedAtConverter)
            .HasColumnName("created_at");
        
        builder
            .Property(typeof(ProductPublishedAt), "_publishedAt")
            .HasConversion(productPublishedAtConverter)
            .HasColumnName("published_at")
            .IsRequired(false);

        builder.HasMany<ProductImage>(p => p.Images)
            .WithOne()
            .HasForeignKey("ProductId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder.Metadata
            .FindNavigation(nameof(Product.Images))
            !.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne<Floristo.Domain.Categories.Entities.Category>().WithMany().HasForeignKey("_categoryId")
            .OnDelete(DeleteBehavior.Restrict);
    }

    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("product_image");
        builder.HasKey(pi => pi.Id);
        
        var productImageUrlConverter = new ValueConverter<ProductImageUrl, string>(piu => piu.Value,
            piu => new(piu));
        var productImageSortOrderConverter = new ValueConverter<ProductImageSortOrder, int>(piso => piso.Value,
            piso => new(piso));
        
        builder
            .Property(p => p.Id)
            .HasConversion(id => id.Value, id => new ProductImageId(id))
            .HasColumnName("id");
        
        builder
            .Property(typeof(ProductImageUrl), "_url")
            .HasConversion(productImageUrlConverter)
            .HasColumnName("url");
        
        builder
            .Property(typeof(ProductImageSortOrder), "_sortOrder")
            .HasConversion(productImageSortOrderConverter)
            .HasColumnName("sort_order");

        builder.Property<ProductId>("ProductId")
            .HasConversion(id => id.Value, id => new ProductId(id))
            .HasColumnName("product_id");
        
        builder.HasIndex("ProductId", "_sortOrder");
    }
}
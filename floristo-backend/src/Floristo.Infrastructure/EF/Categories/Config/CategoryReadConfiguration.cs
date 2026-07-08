using Floristo.Infrastructure.EF.Categories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Floristo.Infrastructure.EF.Categories.Config;

internal sealed class CategoryReadConfiguration : IEntityTypeConfiguration<CategoryReadModel>
{
    public void Configure(EntityTypeBuilder<CategoryReadModel> builder)
    {
        builder.ToTable("category");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.ParentId).HasColumnName("parent_id");
        builder.Property(c => c.Version).HasColumnName("version");
        builder.Property(c => c.Name).HasColumnName("name");
        builder.Property(c => c.ShortDescription).HasColumnName("short_description");
        builder.Property(c => c.Description).HasColumnName("description");
        builder.Property(c => c.Slug).HasColumnName("slug");
        builder.Property(c => c.SortOrder).HasColumnName("sort_order");
        builder.Property(c => c.ImageUrl).HasColumnName("image_url");
        builder.Property(c => c.SeoTitle).HasColumnName("seo_title");
        builder.Property(c => c.SeoDescription).HasColumnName("seo_description");
        builder.Property(c => c.CreatedAt).HasColumnName("created_at");
        builder.Property(c => c.PublishedAt).HasColumnName("published_at");
    }
}
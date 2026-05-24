namespace Floristo.Infrastructure.EF.Products.Models;

public class ProductReadModel
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public decimal? Height { get; set; }
    public decimal? Width { get; set; }
    public decimal? Depth { get; set; }
    public decimal? Diameter { get; set; }
    public decimal? Length { get; set; }
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public bool AllowEngraving { get; set; }
    public bool AllowGiftTag { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UnpublishedAt { get; set; }
    
    public ICollection<ProductImageReadModel> Images { get; set; }
}
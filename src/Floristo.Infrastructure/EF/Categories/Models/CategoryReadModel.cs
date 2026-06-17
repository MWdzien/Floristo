namespace Floristo.Infrastructure.EF.Categories.Models;

public class CategoryReadModel
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Slug { get; set; }
    public int SortOrder { get; set; }
    public string? ImageUrl { get; set; }
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
}
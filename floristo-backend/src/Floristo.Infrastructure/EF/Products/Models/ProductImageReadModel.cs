namespace Floristo.Infrastructure.EF.Products.Models;

public class ProductImageReadModel
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public int SortOrder { get; set; }
    public Guid ProductId { get; set; }
    public ProductReadModel Product { get; set; }
}
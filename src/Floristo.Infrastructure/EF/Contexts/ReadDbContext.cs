using Floristo.Infrastructure.EF.Categories.Config;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Customers.Config;
using Floristo.Infrastructure.EF.Customers.Models;
using Floristo.Infrastructure.EF.Products.Config;
using Floristo.Infrastructure.EF.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Contexts;

internal sealed class ReadDbContext : DbContext
{
    public DbSet<CustomerReadModel> Customers { get; set; }
    public DbSet<CategoryReadModel> Categories { get; set; }
    public DbSet<ProductReadModel> Products { get; set; }

    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("floristo");

        modelBuilder.ApplyConfiguration<CustomerReadModel>(new CustomerReadConfiguration());
        modelBuilder.ApplyConfiguration<CategoryReadModel>(new CategoryReadConfiguration());
        modelBuilder.ApplyConfiguration<ProductReadModel>(new ProductReadConfiguration());
        modelBuilder.ApplyConfiguration<ProductImageReadModel>(new ProductReadConfiguration());
    }
}
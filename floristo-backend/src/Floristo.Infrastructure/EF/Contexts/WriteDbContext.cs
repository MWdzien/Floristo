using Floristo.Domain.Categories.Entities;
using Floristo.Domain.Customers.Entities;
using Floristo.Domain.Products.Entities;
using Floristo.Infrastructure.EF.Categories.Config;
using Floristo.Infrastructure.EF.Customers.Config;
using Floristo.Infrastructure.EF.Products.Config;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Contexts;

internal sealed class WriteDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("floristo");
        
        modelBuilder.ApplyConfiguration<Customer>(new CustomerWriteConfiguration());
        modelBuilder.ApplyConfiguration<Category>(new CategoryWriteConfiguration());
        modelBuilder.ApplyConfiguration<Product>(new ProductWriteConfiguration());
        modelBuilder.ApplyConfiguration<ProductImage>(new ProductWriteConfiguration());
    }
}
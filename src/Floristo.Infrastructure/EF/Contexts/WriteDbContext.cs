using Floristo.Domain.Entities;
using Floristo.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Contexts;

internal sealed class WriteDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public WriteDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("floristo");
        
        modelBuilder.ApplyConfiguration<Customer>(new CustomerWriteConfiguration());
    }
}
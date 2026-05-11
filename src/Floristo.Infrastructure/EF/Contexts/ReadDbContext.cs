using Floristo.Infrastructure.EF.Config;
using Floristo.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Contexts;

internal sealed class ReadDbContext : DbContext
{
    public DbSet<CustomerReadModel> Customers { get; set; }

    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("floristo");

        modelBuilder.ApplyConfiguration<CustomerReadModel>(new CustomerReadConfiguration());
    }
}
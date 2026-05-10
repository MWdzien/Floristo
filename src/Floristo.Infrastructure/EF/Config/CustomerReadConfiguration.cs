using Floristo.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Floristo.Infrastructure.EF.Config;

internal sealed class CustomerReadConfiguration : IEntityTypeConfiguration<CustomerReadModel>
{
    public void Configure(EntityTypeBuilder<CustomerReadModel> builder)
    {
        builder.ToTable("customer");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.Version).HasColumnName("version");
        builder.Property(c => c.FirstName).HasColumnName("first_name");
        builder.Property(c => c.LastName).HasColumnName("last_name");
        builder.Property(c => c.Email).HasColumnName("email");
        builder.Property(c => c.CreatedAt).HasColumnName("created_at");
        builder.Property(c => c.DeactivatedAt).HasColumnName("deactivated_at");
    }
}
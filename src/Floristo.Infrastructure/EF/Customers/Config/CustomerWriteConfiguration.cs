using Floristo.Domain.Customers.Entities;
using Floristo.Domain.Customers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Floristo.Infrastructure.EF.Customers.Config;

internal sealed class CustomerWriteConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customer");
        builder.HasKey(x => x.Id);

        var customerFirstNameConverter = new ValueConverter<CustomerFirstName, string>(cfn => cfn.Value,
            cfn => new CustomerFirstName(cfn));
        
        var customerLastNameConverter = new ValueConverter<CustomerLastName, string>(cln => cln.Value,
            cln => new CustomerLastName(cln));
        
        var customerEmailConverter = new ValueConverter<CustomerEmail, string>(ce => ce.Value,
            ce => new CustomerEmail(ce));
        
        var customerCreatedAtConverter = new ValueConverter<CustomerCreatedAt, DateTime>(cca => cca.Value,
            cca => new CustomerCreatedAt(cca));
        
        var customerDeactivatedAtConverter = new ValueConverter<CustomerDeactivatedAt, DateTime>(cda => cda.Value,
            cda => new CustomerDeactivatedAt(cda));
        
        builder
            .Property(c => c.Id)
            .HasConversion(id => id.Value, id => new CustomerId(id))
            .HasColumnName("id");

        builder
            .Property(c => c.Version)
            .HasColumnName("version")
            .IsConcurrencyToken();

        builder
            .Property(typeof(CustomerFirstName), "_firstName")
            .HasConversion(customerFirstNameConverter)
            .HasColumnName("first_name");
        
        builder
            .Property(typeof(CustomerLastName), "_lastName")
            .HasConversion(customerLastNameConverter)
            .HasColumnName("last_name");
        
        builder
            .Property(typeof(CustomerEmail), "_email")
            .HasConversion(customerEmailConverter)
            .HasColumnName("email");
        
        builder
            .Property(typeof(CustomerCreatedAt), "_createdAt")
            .HasConversion(customerCreatedAtConverter)
            .HasColumnName("created_at");
        
        builder
            .Property(typeof(CustomerDeactivatedAt), "_deactivatedAt")
            .HasConversion(customerDeactivatedAtConverter)
            .HasColumnName("deactivated_at")
            .IsRequired(false);
    }
}
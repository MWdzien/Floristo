namespace Floristo.Application.Customers.DTOs;

public record BackofficeCustomerDetailsDto(Guid Id, string FirstName, string? LastName, string Email, DateTime CreatedAt, DateTime? DeactivatedAt);
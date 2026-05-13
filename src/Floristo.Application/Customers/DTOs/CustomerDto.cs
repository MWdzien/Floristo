namespace Floristo.Application.Customers.DTOs;

public record CustomerDto(Guid Id, string FirstName, string LastName, string Email, DateTime CreatedAt, DateTime? DeactivatedAt);
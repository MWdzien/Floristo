namespace Floristo.Application.Customers.DTOs;

public record BackofficeCustomerDto(Guid Id, string FullName, string Email, bool IsActive);
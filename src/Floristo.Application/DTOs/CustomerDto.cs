namespace Floristo.Application.DTOs;

public record CustomerDto(Guid Id, string FirstName, string LastName, string? Email, bool? IsActive);
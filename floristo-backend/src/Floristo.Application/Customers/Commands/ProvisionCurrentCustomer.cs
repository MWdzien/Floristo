using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands;

public record ProvisionCurrentCustomer(string FirstName, string? LastName) : ICommand;
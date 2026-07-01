using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class CannotRemoveLastImageOfPublishedProductException() : FloristoException("Cannot remove the last Image of Product if the Product is published");
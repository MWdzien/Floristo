using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class InvalidProductDimensionException(string dimensionName) : FloristoException($"product {dimensionName} must be between 0 and 500 centimeters");
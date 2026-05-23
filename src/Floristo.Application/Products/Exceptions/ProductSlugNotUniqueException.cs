using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Products.Exceptions;

public class ProductSlugNotUniqueException(string slug)
    : FloristoException($"Product with slug: {slug} already exists")
{
    public override int StatusCode => 409;
}
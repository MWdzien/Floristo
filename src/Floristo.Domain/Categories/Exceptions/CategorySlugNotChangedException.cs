using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class CategorySlugNotChangedException() : FloristoException("provided category slug is the same as previous")
{
    public override int StatusCode => 422;
}
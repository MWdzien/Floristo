using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Products.Exceptions;

public class CategoryHasProductsException() : FloristoException("Category can not have any products assigned before deletion");
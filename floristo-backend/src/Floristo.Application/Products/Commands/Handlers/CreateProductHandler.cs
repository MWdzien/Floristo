using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
using Floristo.Application.Products.Exceptions;
using Floristo.Application.Products.Services;
using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.Factories;
using Floristo.Domain.Products.Repositories;
using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands.Handlers;

internal sealed class CreateProductHandler : ICommandHandler<CreateProduct>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductFactory _productFactory;
    private readonly IProductReadService _productReadService;
    private readonly ICategoryReadService _categoryReadService;

    public CreateProductHandler(IProductRepository productRepository, IProductFactory productFactory, IProductReadService productReadService, ICategoryReadService categoryReadService)
    {
        _productRepository = productRepository;
        _productFactory = productFactory;
        _productReadService = productReadService;
        _categoryReadService = categoryReadService;
    }

    public async Task HandleAsync(CreateProduct command)
    {
        var id = Guid.NewGuid();
        var (name, slug, price, categoryId, sortOrder) = command;

        if (await _productReadService.ExistsBySlugAsync(slug))
            throw new ProductSlugNotUniqueException(slug);
        
        if (!await _categoryReadService.ExistsAsync(categoryId))
            throw new CategoryNotFoundException(categoryId);
        
        if (await _categoryReadService.HasChildrenAsync(categoryId))
            throw new CategoryWithChildrenCannotHaveProductsException(categoryId);
        

        var product = _productFactory.Create(
            new ProductId(id),
            new ProductName(name),
            new ProductSlug(slug),
            new ProductPrice(price),
            new CategoryId(categoryId),
            new ProductSortOrder(sortOrder)
        );

        await _productRepository.AddAsync(product);
    }
}
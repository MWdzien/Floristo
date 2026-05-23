using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
using Floristo.Application.Products.Exceptions;
using Floristo.Domain.Products.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands.Handlers;

internal sealed class ChangeProductCategoryHandler : ICommandHandler<ChangeProductCategory>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryReadService _categoryReadService;

    public ChangeProductCategoryHandler(IProductRepository productRepository, ICategoryReadService categoryReadService)
    {
        _productRepository = productRepository;
        _categoryReadService = categoryReadService;
    }

    public async Task HandleAsync(ChangeProductCategory command)
    {
        var product = await _productRepository.GetAsync(command.Id);

        if (product is null)
            throw new ProductNotFoundException(command.Id);

        if (!await _categoryReadService.ExistsAsync(command.CategoryId))
            throw new CategoryNotFoundException(command.CategoryId);
        
        product.ChangeCategory(command.CategoryId);

        await _productRepository.UpdateAsync(product);
    }
}
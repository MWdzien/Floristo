using Floristo.Application.Products.Exceptions;
using Floristo.Application.Products.Services;
using Floristo.Domain.Products.Repositories;
using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands.Handlers;

internal sealed class UpdateProductDetailsHandler : ICommandHandler<UpdateProductDetails>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductReadService _productReadService;

    public UpdateProductDetailsHandler(IProductRepository productRepository, IProductReadService productReadService)
    {
        _productRepository = productRepository;
        _productReadService = productReadService;
    }

    public async Task HandleAsync(UpdateProductDetails command)
    {
        var (id, name, slug, description, height, width, 
            depth, diameter, length, seoTitle, seoDescription) = command;
        
        var product = await _productRepository.GetAsync(id);

        if (product is null)
            throw new ProductNotFoundException(id);

        if (await _productReadService.ExistsBySlugAsync(slug, id))
            throw new ProductSlugNotUniqueException(slug);
        
        product.ChangeName(name);
        product.ChangeSlug(slug);
        
        if (string.IsNullOrWhiteSpace(description))
            product.DeleteDescription();
        else product.ChangeDescription(description);
        
        if (height is null && width is null && depth is null && diameter is null && length is null)
            product.DeleteDimensions();
        else product.ChangeDimensions(new ProductDimensions(height, width, depth, diameter, length));
        
        if (string.IsNullOrWhiteSpace(seoTitle))
            product.DeleteSeoTitle();
        else product.ChangeSeoTitle(seoTitle);
        
        if (string.IsNullOrWhiteSpace(seoDescription))
            product.DeleteSeoDescription();
        else product.ChangeSeoDescription(seoDescription);

        await _productRepository.UpdateAsync(product);
    }
}
using Floristo.Application.Products.Exceptions;
using Floristo.Domain.Products.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands.Handlers;

internal sealed class UnpublishProductHandler : ICommandHandler<UnpublishProduct>
{
    private readonly IProductRepository _productRepository;

    public UnpublishProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task HandleAsync(UnpublishProduct command)
    {
        var product = await _productRepository.GetAsync(command.Id);

        if (product is null)
            throw new ProductNotFoundException(command.Id);
        
        product.Unpublish();

        await _productRepository.UpdateAsync(product);
    }
}
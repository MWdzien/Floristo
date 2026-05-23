using Floristo.Application.Products.Exceptions;
using Floristo.Domain.Products.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands.Handlers;

internal sealed class PublishProductHandler : ICommandHandler<PublishProduct>
{
    private readonly IProductRepository _productRepository;

    public PublishProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task HandleAsync(PublishProduct command)
    {
        var product = await _productRepository.GetAsync(command.Id);

        if (product is null)
            throw new ProductNotFoundException(command.Id);
        
        product.Publish();

        await _productRepository.UpdateAsync(product);
    }
}
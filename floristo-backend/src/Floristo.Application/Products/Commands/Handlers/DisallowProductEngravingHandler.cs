using Floristo.Application.Products.Exceptions;
using Floristo.Domain.Products.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands.Handlers;

internal sealed class DisallowProductEngravingHandler : ICommandHandler<DisallowProductEngraving>
{
    private readonly IProductRepository _productRepository;

    public DisallowProductEngravingHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task HandleAsync(DisallowProductEngraving command)
    {
        var product = await _productRepository.GetAsync(command.Id);

        if (product is null)
            throw new ProductNotFoundException(command.Id);
        
        product.DisallowEngraving();

        await _productRepository.UpdateAsync(product);
    }
}
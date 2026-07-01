using Floristo.Application.Products.Exceptions;
using Floristo.Domain.Products.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands.Handlers;

internal sealed class ChangeProductPriceHandler : ICommandHandler<ChangeProductPrice>
{
    private readonly IProductRepository _productRepository;

    public ChangeProductPriceHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task HandleAsync(ChangeProductPrice command)
    {
        var product = await _productRepository.GetAsync(command.Id);

        if (product is null)
            throw new ProductNotFoundException(command.Id);
        
        product.ChangePrice(command.Price);

        await _productRepository.UpdateAsync(product);
    }
}
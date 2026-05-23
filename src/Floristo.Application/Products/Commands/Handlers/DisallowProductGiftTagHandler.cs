using Floristo.Application.Products.Exceptions;
using Floristo.Domain.Products.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands.Handlers;

internal sealed class DisallowProductGiftTagHandler : ICommandHandler<DisallowProductGiftTag>
{
    private readonly IProductRepository _productRepository;

    public DisallowProductGiftTagHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task HandleAsync(DisallowProductGiftTag command)
    {
        var product = await _productRepository.GetAsync(command.Id);

        if (product is null)
            throw new ProductNotFoundException(command.Id);
        
        product.DisallowGiftTag();

        await _productRepository.UpdateAsync(product);
    }
}
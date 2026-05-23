using Floristo.Application.Products.Exceptions;
using Floristo.Domain.Products.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands.Handlers;

internal sealed class AllowProductEngravingHandler : ICommandHandler<AllowProductEngraving>
{
    private readonly IProductRepository _productRepository;

    public AllowProductEngravingHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task HandleAsync(AllowProductEngraving command)
    {
        var product = await _productRepository.GetAsync(command.Id);

        if (product is null)
            throw new ProductNotFoundException(command.Id);
        
        product.AllowEngraving();

        await _productRepository.UpdateAsync(product);
    }
}
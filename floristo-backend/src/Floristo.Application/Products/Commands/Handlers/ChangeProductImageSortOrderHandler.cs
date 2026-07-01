using Floristo.Application.Products.Exceptions;
using Floristo.Domain.Products.Repositories;
using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands.Handlers;

internal sealed class ChangeProductImageSortOrderHandler : ICommandHandler<ChangeProductImageSortOrder>
{
    private readonly IProductRepository _productRepository;

    public ChangeProductImageSortOrderHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task HandleAsync(ChangeProductImageSortOrder command)
    {
        var product = await _productRepository.GetAsync(command.Id);

        if (product is null)
            throw new ProductNotFoundException(command.Id);
        
        product.ChangeImageSortOrder(command.ImageId, command.SortOrder);

        await _productRepository.UpdateAsync(product);
    }
}
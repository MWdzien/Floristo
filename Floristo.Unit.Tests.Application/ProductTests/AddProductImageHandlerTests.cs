using Floristo.Application.Products.Commands;
using Floristo.Application.Products.Commands.Handlers;
using Floristo.Application.Products.Exceptions;
using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.Factories;
using Floristo.Domain.Products.Repositories;
using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Commands;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Application.ProductTests;

public class AddProductImageHandlerTests
{
    Task Act(AddProductImage command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_ProductNotFoundException_When_Product_Does_Not_Exist()
    {
        var command = new AddProductImage(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "https://cdn.test/a.jpg",
            1);

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = new AddProductImage(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "https://cdn.test/a.jpg",
            1);
        var product = new ProductFactory().Create(
            new ProductId(command.Id),
            new ProductName("Test"),
            new ProductSlug("test"),
            new ProductPrice(99.99m),
            new CategoryId(Guid.NewGuid()),
            new ProductSortOrder(1));
        _repository.GetAsync(command.Id).Returns(product);

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldBeNull();
        await _repository.Received(1).UpdateAsync(product);
    }

    #region ARRANGE

    private readonly ICommandHandler<AddProductImage> _commandHandler;
    private readonly IProductRepository _repository;

    public AddProductImageHandlerTests()
    {
        _repository = Substitute.For<IProductRepository>();

        _commandHandler = new AddProductImageHandler(_repository);
    }

    #endregion
}

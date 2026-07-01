using Floristo.Application.Products.Commands;
using Floristo.Application.Products.Commands.Handlers;
using Floristo.Application.Products.Exceptions;
using Floristo.Application.Products.Services;
using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.Factories;
using Floristo.Domain.Products.Repositories;
using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Commands;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Application.ProductTests;

public class UpdateProductDetailsHandlerTests
{
    Task Act(UpdateProductDetails command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_ProductNotFoundException_When_Product_Does_Not_Exist()
    {
        var command = GetCommand();

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_Throws_ProductSlugNotUniqueException_When_Slug_Is_Not_Unique()
    {
        var command = GetCommand();
        var product = new ProductFactory().Create(
            new ProductId(command.Id),
            new ProductName("Old name"),
            new ProductSlug("old-slug"),
            new ProductPrice(99.99m),
            new CategoryId(Guid.NewGuid()),
            new ProductSortOrder(1));
        _repository.GetAsync(command.Id).Returns(product);
        _readService.ExistsBySlugAsync(command.Slug, command.Id).Returns(true);

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductSlugNotUniqueException>();
    }

    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = GetCommand();
        var product = new ProductFactory().Create(
            new ProductId(command.Id),
            new ProductName("Old name"),
            new ProductSlug("old-slug"),
            new ProductPrice(99.99m),
            new CategoryId(Guid.NewGuid()),
            new ProductSortOrder(1));
        _repository.GetAsync(command.Id).Returns(product);
        _readService.ExistsBySlugAsync(command.Slug, command.Id).Returns(false);

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldBeNull();
        await _repository.Received(1).UpdateAsync(product);
    }

    #region ARRANGE

    private UpdateProductDetails GetCommand()
    {
        return new UpdateProductDetails(
            Guid.NewGuid(),
            "New name",
            "new-slug",
            null,
            null, null, null, null, null,
            null,
            null);
    }

    private readonly ICommandHandler<UpdateProductDetails> _commandHandler;
    private readonly IProductRepository _repository;
    private readonly IProductReadService _readService;

    public UpdateProductDetailsHandlerTests()
    {
        _repository = Substitute.For<IProductRepository>();
        _readService = Substitute.For<IProductReadService>();

        _commandHandler = new UpdateProductDetailsHandler(_repository, _readService);
    }

    #endregion
}

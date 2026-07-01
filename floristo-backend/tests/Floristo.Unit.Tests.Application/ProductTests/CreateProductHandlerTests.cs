using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
using Floristo.Application.Products.Commands;
using Floristo.Application.Products.Commands.Handlers;
using Floristo.Application.Products.Exceptions;
using Floristo.Application.Products.Services;
using Floristo.Domain.Products.Entities;
using Floristo.Domain.Products.Factories;
using Floristo.Domain.Products.Repositories;
using Floristo.SharedAbstractions.Commands;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Application.ProductTests;

public class CreateProductHandlerTests
{
    Task Act(CreateProduct command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_ProductSlugNotUniqueException_When_Slug_Already_Exists()
    {
        var command = GetCommand();
        _productReadService.ExistsBySlugAsync(command.Slug).Returns(true);

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductSlugNotUniqueException>();
    }

    [Fact]
    public async Task HandleAsync_Throws_CategoryNotFoundException_When_Category_Does_Not_Exist()
    {
        var command = GetCommand();
        _productReadService.ExistsBySlugAsync(command.Slug).Returns(false);
        _categoryReadService.ExistsAsync(command.CategoryId).Returns(false);

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategoryNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = GetCommand();
        _productReadService.ExistsBySlugAsync(command.Slug).Returns(false);
        _categoryReadService.ExistsAsync(command.CategoryId).Returns(true);
        _factory.Create(default!, default!, default!, default!, default!, default!).ReturnsForAnyArgs(default(Product));

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldBeNull();
        await _repository.Received(1).AddAsync(Arg.Any<Product>());
    }

    #region ARRANGE

    private CreateProduct GetCommand()
    {
        return new CreateProduct(
            Guid.NewGuid(),
            "Test Product",
            "test-product",
            99.99m,
            Guid.NewGuid(),
            1);
    }

    private readonly ICommandHandler<CreateProduct> _commandHandler;
    private readonly IProductRepository _repository;
    private readonly IProductFactory _factory;
    private readonly IProductReadService _productReadService;
    private readonly ICategoryReadService _categoryReadService;

    public CreateProductHandlerTests()
    {
        _repository = Substitute.For<IProductRepository>();
        _factory = Substitute.For<IProductFactory>();
        _productReadService = Substitute.For<IProductReadService>();
        _categoryReadService = Substitute.For<ICategoryReadService>();

        _commandHandler = new CreateProductHandler(_repository, _factory, _productReadService, _categoryReadService);
    }

    #endregion
}

using Floristo.Application.Categories.Commands;
using Floristo.Application.Categories.Commands.Handlers;
using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
using Floristo.Application.Products.Services;
using Floristo.Domain.Categories.Entities;
using Floristo.Domain.Categories.Factories;
using Floristo.Domain.Categories.Repositories;
using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Commands;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Application.CategoryTests;

public class CreateCategoryHandlerTests
{
    Task Act(CreateCategory command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_CategorySlugNotUniqueException_When_Category_With_Same_Slug_Exists()
    {
        var command = GetCommand();
        _readService.ExistsBySlugAsync(command.Slug).Returns(true);

        var exception = await Record.ExceptionAsync(() => Act(command));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategorySlugNotUniqueException>();
    }

    [Fact]
    public async Task HandleAsync_Throws_CategoryNotFoundException_When_Parent_Does_Not_Exist()
    {
        var parentId = Guid.NewGuid();
        var command = GetCommand(parentId);
        _readService.ExistsBySlugAsync(command.Slug).Returns(false);
        _readService.ExistsAsync(parentId).Returns(false);

        var exception = await Record.ExceptionAsync(() => Act(command));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategoryNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_Throws_CategoryParentMustBeRootException_When_Parent_Is_Not_Root()
    {
        var parentId = Guid.NewGuid();
        var command = GetCommand(parentId);
        _readService.ExistsBySlugAsync(command.Slug).Returns(false);
        _readService.ExistsAsync(parentId).Returns(true);
        _readService.IsRootAsync(parentId).Returns(false);

        var exception = await Record.ExceptionAsync(() => Act(command));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategoryParentMustBeRootException>();
    }

    [Fact]
    public async Task HandleAsync_Throws_CategoryWithProductsCannotBecomeParentException_When_Parent_Has_Products()
    {
        var parentId = Guid.NewGuid();
        var command = GetCommand(parentId);
        _readService.ExistsBySlugAsync(command.Slug).Returns(false);
        _readService.ExistsAsync(parentId).Returns(true);
        _readService.IsRootAsync(parentId).Returns(true);
        _productReadService.ExistsByCategoryAsync(parentId).Returns(true);

        var exception = await Record.ExceptionAsync(() => Act(command));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategoryWithProductsCannotBecomeParentException>();
    }

    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = GetCommand();
        _readService.ExistsBySlugAsync(command.Slug).Returns(false);
        _readService.GetNextSortOrderAsync(command.ParentId).Returns(0);
        _factory.Create(Arg.Any<CategoryId>(), Arg.Any<CategoryId?>(), Arg.Any<CategoryName>(),
            Arg.Any<CategorySlug>(), Arg.Any<CategorySortOrder>()).Returns(default(Category));

        var exception = await Record.ExceptionAsync(() => Act(command));
        exception.ShouldBeNull();
        await _repository.Received(1).AddAsync(Arg.Any<Category>());
    }


    #region ARRANGE

    public CreateCategory GetCommand(Guid? parentId = null)
    {
        return new CreateCategory(parentId, "Test Flowers", "test-flowers");
    }

    private readonly ICommandHandler<CreateCategory> _commandHandler;
    private readonly ICategoryRepository _repository;
    private readonly ICategoryFactory _factory;
    private readonly ICategoryReadService _readService;
    private readonly IProductReadService _productReadService;

    public CreateCategoryHandlerTests()
    {
        _repository = Substitute.For<ICategoryRepository>();
        _factory = Substitute.For<ICategoryFactory>();
        _readService = Substitute.For<ICategoryReadService>();
        _productReadService = Substitute.For<IProductReadService>();

        _commandHandler = new CreateCategoryHandler(_repository, _factory, _readService, _productReadService);
    }

    #endregion
}

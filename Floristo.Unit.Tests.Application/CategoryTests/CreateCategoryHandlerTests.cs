using Floristo.Application.Categories.Commands;
using Floristo.Application.Categories.Commands.Handlers;
using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
using Floristo.Domain.Categories.Entities;
using Floristo.Domain.Categories.Factories;
using Floristo.Domain.Categories.Repositories;
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
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = GetCommand();
        _readService.ExistsBySlugAsync(command.Slug).Returns(false);
        _factory.Create(command.Id, command.Name, command.Slug, command.SortOrder).Returns(default(Category));
        
        var exception = await Record.ExceptionAsync(() => Act(command));
        exception.ShouldBeNull();
        await _repository.Received(1).AddAsync(Arg.Any<Category>());
    }
    
    
    #region ARRANGE

    public CreateCategory GetCommand()
    {
        return new CreateCategory(Guid.NewGuid(), "Test Flowers", "test-flowers", 1);
    }
    
    private readonly ICommandHandler<CreateCategory> _commandHandler;
    private readonly ICategoryRepository _repository;
    private readonly ICategoryFactory _factory;
    private readonly ICategoryReadService _readService;

    public CreateCategoryHandlerTests()
    {
        _repository = Substitute.For<ICategoryRepository>();
        _factory = Substitute.For<ICategoryFactory>();
        _readService = Substitute.For<ICategoryReadService>();
        
        _commandHandler = new CreateCategoryHandler(_repository, _factory, _readService);
    }

    #endregion
}
using Floristo.Application.Categories.Commands;
using Floristo.Application.Categories.Commands.Handlers;
using Floristo.Application.Categories.Exceptions;
using Floristo.Application.Categories.Services;
using Floristo.Domain.Categories.Factories;
using Floristo.Domain.Categories.Repositories;
using Floristo.SharedAbstractions.Commands;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Application.CategoryTests;

public class UpdateCategoryDetailsHandlerTests
{
    Task Act(UpdateCategoryDetails command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_CategoryNotFoundException_When_Category_Does_Not_Exist()
    {
        var command = GetCommand();

        var exception = await Record.ExceptionAsync(async () => await Act(command));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategoryNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_Throws_CategorySlugNotUniqueException_When_Slug_Is_Not_Unique()
    {
        var command = GetCommand();
        var category = new CategoryFactory().Create(command.Id, null, "Old name", "old-slug", 1);
        _repository.GetAsync(command.Id).Returns(category);
        _readService.ExistsBySlugAsync(command.Slug, command.Id).Returns(true);

        var exception = await Record.ExceptionAsync(async () => await Act(command));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategorySlugNotUniqueException>();
    }

    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = GetCommand();
        var category = new CategoryFactory().Create(command.Id, null, "Old name", "old-slug", 1);
        _repository.GetAsync(command.Id).Returns(category);
        _readService.ExistsBySlugAsync(command.Slug, command.Id).Returns(false);

        var exception = await Record.ExceptionAsync(async () => await Act(command));
        exception.ShouldBeNull();
        await _repository.Received(1).UpdateAsync(category);
    }

    #region ARRANGE

    private UpdateCategoryDetails GetCommand()
    {
        return new UpdateCategoryDetails(
            Guid.NewGuid(),
            "New name",
            "new-slug",
            null,
            null,
            null,
            null);
    }

    private readonly ICommandHandler<UpdateCategoryDetails> _commandHandler;
    private readonly ICategoryRepository _repository;
    private readonly ICategoryReadService _readService;

    public UpdateCategoryDetailsHandlerTests()
    {
        _repository = Substitute.For<ICategoryRepository>();
        _readService = Substitute.For<ICategoryReadService>();

        _commandHandler = new UpdateCategoryDetailsHandler(_repository, _readService);
    }

    #endregion
}

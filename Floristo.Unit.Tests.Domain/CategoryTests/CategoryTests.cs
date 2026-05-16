using System.Net.Http.Headers;
using Floristo.Domain.Categories.Entities;
using Floristo.Domain.Categories.Events;
using Floristo.Domain.Categories.Exceptions;
using Floristo.Domain.Categories.Factories;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Domain.CategoryTests;

public class CategoryTests
{
    [Fact]
    public void ChangeSlug_Throws_CategorySlugAlreadyInUseException_When_Slug_Is_Already_In_Use()
    {
        var category = GetCategory();
        
        var exception = Record.Exception(() => category.ChangeSlug("test-flowers"));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategorySlugAlreadyInUseException>();
    }

    [Fact]
    public void ChangeSlug_Adds_CategorySlugChangedEvent_Domain_Event_On_Success()
    {
        var category = GetCategory();
        
        var exception = Record.Exception(() => category.ChangeSlug("test-slug"));
        
        exception.ShouldBeNull();
        category.Events.Count().ShouldBe(1);
        var @event = category.Events.FirstOrDefault() as CategorySlugChangedEvent;
        @event.ShouldNotBeNull();
        @event.Slug.Value.ShouldBe("test-slug");
    }
    
    [Fact]
    public void Activate_Throws_CategoryAlreadyActiveException_When_Category_Is_Already_Active()
    {
        var category = GetCategory();
        category.Activate();
        
        
        var exception = Record.Exception(() => category.Activate());
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategoryAlreadyActiveException>();
    }
    
    [Fact]
    public void Activate_Adds_CategoryActivatedEvent_Domain_Event_On_Success()
    {
        var category = GetCategory();
        
        var exception = Record.Exception(() => category.Activate());
        
        exception.ShouldBeNull();
        category.Events.Count().ShouldBe(1);
        var @event = category.Events.FirstOrDefault() as CategoryActivatedEvent;
        @event.ShouldNotBeNull();
    }

    [Fact]
    public void Deactivate_Throws_CategoryAlreadyDeactivatedException_When_Category_Is_Already_Deactivated()
    {
        var category = GetCategory();
        
        var exception = Record.Exception(() => category.Deactivate());
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategoryAlreadyDeactivatedException>();
    }
    
    [Fact]
    public void Deactivate_Adds_CategoryDeactivatedEvent_Domain_Event_On_Success()
    {
        var category = GetCategory();
        category.Activate();
        category.ClearEvents();
        
        var exception = Record.Exception(() => category.Deactivate());
        
        exception.ShouldBeNull();
        category.Events.Count().ShouldBe(1);
        var @event = category.Events.FirstOrDefault() as CategoryDeactivatedEvent;
        @event.ShouldNotBeNull();
    }
    
    [Fact]
    public void Delete_Throws_CannotDeleteActiveCategoryException_When_Category_Is_Active()
    {
        var category = GetCategory();
        category.Activate();
        
        var exception = Record.Exception(() => category.Delete());
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CannotDeleteActiveCategoryException>();
    }

    [Fact]
    public void Delete_Throws_CategoryDeletedEvent_Domain_Event_On_Success()
    {
        var category = GetCategory();
        
        var exception = Record.Exception(() => category.Delete());
        
        exception.ShouldBeNull();
        category.Events.Count().ShouldBe(1);
        var @event = category.Events.FirstOrDefault() as CategoryDeletedEvent;
        @event.ShouldNotBeNull();
    }
    
    
    #region ARRANGE

    private Category GetCategory()
    {
        var category = _categoryFactory
            .Create(Guid.NewGuid(), "Test Flowers", "test-flowers", 1);
        
        category.ClearEvents();
        return category;
    }
    
    private readonly ICategoryFactory _categoryFactory;

    public CategoryTests()
    {
        _categoryFactory = new CategoryFactory();
    }

    #endregion
}
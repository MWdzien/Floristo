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
    public void ChangeSlug_Throws_CategorySlugNotChangedException_When_Slug_Is_Not_Changed()
    {
        var category = GetCategory();
        
        var exception = Record.Exception(() => category.ChangeSlug("test-flowers"));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategorySlugNotChangedException>();
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
    public void Activate_Throws_CategoryAlreadyPublishedException_When_Category_Is_Already_Published()
    {
        var category = GetCategory();
        category.Publish();
        
        
        var exception = Record.Exception(() => category.Publish());
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategoryAlreadyPublishedException>();
    }
    
    [Fact]
    public void Publish_Adds_CategoryPublishedEvent_Domain_Event_On_Success()
    {
        var category = GetCategory();
        
        var exception = Record.Exception(() => category.Publish());
        
        exception.ShouldBeNull();
        category.Events.Count().ShouldBe(1);
        var @event = category.Events.FirstOrDefault() as CategoryPublishedEvent;
        @event.ShouldNotBeNull();
    }

    [Fact]
    public void Unpublish_Throws_CategoryAlreadyUnpublishedException_When_Category_Is_Already_Unpublished()
    {
        var category = GetCategory();
        
        var exception = Record.Exception(() => category.Unpublish());
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CategoryAlreadyUnpublishedException>();
    }
    
    [Fact]
    public void Deactivate_Adds_CategoryUnpublishedEvent_Domain_Event_On_Success()
    {
        var category = GetCategory();
        category.Publish();
        category.ClearEvents();
        
        var exception = Record.Exception(() => category.Unpublish());
        
        exception.ShouldBeNull();
        category.Events.Count().ShouldBe(1);
        var @event = category.Events.FirstOrDefault() as CategoryUnpublishedEvent;
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
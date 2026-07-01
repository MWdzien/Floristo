using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.Entities;
using Floristo.Domain.Products.Events;
using Floristo.Domain.Products.Exceptions;
using Floristo.Domain.Products.Exceptions.Lifecycle;
using Floristo.Domain.Products.Exceptions.Validation.Image;
using Floristo.Domain.Products.Factories;
using Floristo.Domain.Products.ValueObjects;
using Floristo.Domain.Products.ValueObjects.ProductImage;
using Shouldly;
using Xunit;

namespace Floristo.Unit.Tests.Domain.ProductTests;

public class ProductTests
{
    [Fact]
    public void ChangeName_Does_Not_Add_Event_When_Name_Is_Not_Changed()
    {
        var product = GetProduct();

        product.ChangeName(new ProductName("Test Product"));

        product.Events.ShouldBeEmpty();
    }

    [Fact]
    public void ChangeName_Adds_ProductNameChangedEvent_Domain_Event_On_Success()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.ChangeName(new ProductName("New Name")));

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);
        var @event = product.Events.FirstOrDefault() as ProductNameChangedEvent;
        @event.ShouldNotBeNull();
        @event.Name.Value.ShouldBe("New Name");
    }

    [Fact]
    public void ChangeSlug_Does_Not_Add_Event_When_Slug_Is_Not_Changed()
    {
        var product = GetProduct();

        product.ChangeSlug(new ProductSlug("test-product"));

        product.Events.ShouldBeEmpty();
    }

    [Fact]
    public void ChangeSlug_Adds_ProductSlugChangedEvent_Domain_Event_On_Success()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.ChangeSlug(new ProductSlug("new-slug")));

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);
        var @event = product.Events.FirstOrDefault() as ProductSlugChangedEvent;
        @event.ShouldNotBeNull();
        @event.Slug.Value.ShouldBe("new-slug");
    }

    [Fact]
    public void ChangePrice_Adds_ProductPriceChangedEvent_Domain_Event_On_Success()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.ChangePrice(new ProductPrice(199.99m)));

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);
        var @event = product.Events.FirstOrDefault() as ProductPriceChangedEvent;
        @event.ShouldNotBeNull();
        @event.Price.Value.ShouldBe(199.99m);
    }

    [Fact]
    public void ChangeCategory_Adds_ProductCategoryChangedEvent_Domain_Event_On_Success()
    {
        var product = GetProduct();
        var newCategoryId = new CategoryId(Guid.NewGuid());

        var exception = Record.Exception(() => product.ChangeCategory(newCategoryId));

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);
        var @event = product.Events.FirstOrDefault() as ProductCategoryChangedEvent;
        @event.ShouldNotBeNull();
        @event.CategoryId.Value.ShouldBe(newCategoryId.Value);
    }

    [Fact]
    public void ChangeSortOrder_Adds_ProductSortOrderChangedEvent_Domain_Event_On_Success()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.ChangeSortOrder(new ProductSortOrder(5)));

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);
        var @event = product.Events.FirstOrDefault() as ProductSortOrderChangedEvent;
        @event.ShouldNotBeNull();
        @event.SortOrder.Value.ShouldBe(5);
    }

    [Fact]
    public void DeleteDescription_Does_Not_Add_Event_When_Description_Is_Already_Null()
    {
        var product = GetProduct();

        product.DeleteDescription();

        product.Events.ShouldBeEmpty();
    }

    [Fact]
    public void AllowEngraving_Throws_ProductEngravingAlreadyAllowedException_When_Already_Allowed()
    {
        var product = GetProduct();
        product.AllowEngraving();

        var exception = Record.Exception(() => product.AllowEngraving());

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductEngravingAlreadyAllowedException>();
    }

    [Fact]
    public void AllowEngraving_Adds_ProductEngravingAllowedEvent_Domain_Event_On_Success()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.AllowEngraving());

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);
        product.Events.FirstOrDefault().ShouldBeOfType<ProductEngravingAllowedEvent>();
    }

    [Fact]
    public void DisallowEngraving_Throws_ProductEngravingAlreadyDisallowedException_When_Already_Disallowed()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.DisallowEngraving());

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductEngravingAlreadyDisallowedException>();
    }

    [Fact]
    public void AllowGiftTag_Throws_ProductGiftTagAlreadyAllowedException_When_Already_Allowed()
    {
        var product = GetProduct();
        product.AllowGiftTag();

        var exception = Record.Exception(() => product.AllowGiftTag());

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductGiftTagAlreadyAllowedException>();
    }

    [Fact]
    public void DisallowGiftTag_Throws_ProductGiftTagAlreadyDisallowedException_When_Already_Disallowed()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.DisallowGiftTag());

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductGiftTagAlreadyDisallowedException>();
    }

    [Fact]
    public void Unpublish_Throws_ProductAlreadyUnpublishedException_When_Already_Unpublished()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.Unpublish());

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductAlreadyUnpublishedException>();
    }

    [Fact]
    public void Publish_Throws_CannotPublishProductWithoutImagesException_When_No_Images()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.Publish());

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CannotPublishProductWithoutImagesException>();
    }

    [Fact]
    public void Publish_Throws_ProductAlreadyPublishedException_When_Already_Published()
    {
        var product = GetProductWithImage();
        product.Publish();

        var exception = Record.Exception(() => product.Publish());

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductAlreadyPublishedException>();
    }

    [Fact]
    public void Publish_Adds_ProductPublishedEvent_Domain_Event_On_Success()
    {
        var product = GetProductWithImage();

        var exception = Record.Exception(() => product.Publish());

        exception.ShouldBeNull();
        product.Events.OfType<ProductPublishedEvent>().Count().ShouldBe(1);
    }

    [Fact]
    public void Unpublish_Adds_ProductUnpublishedEvent_Domain_Event_On_Success()
    {
        var product = GetProductWithImage();
        product.Publish();
        product.ClearEvents();

        var exception = Record.Exception(() => product.Unpublish());

        exception.ShouldBeNull();
        product.Events.OfType<ProductUnpublishedEvent>().Count().ShouldBe(1);
    }

    [Fact]
    public void AddImage_Throws_ProductImageAlreadyExistsException_When_Image_With_Same_Id_Exists()
    {
        var product = GetProduct();
        var imageId = new ProductImageId(Guid.NewGuid());
        product.AddImage(imageId, new ProductImageUrl("https://cdn.test/a.jpg"), new ProductImageSortOrder(1));

        var exception = Record.Exception(() =>
            product.AddImage(imageId, new ProductImageUrl("https://cdn.test/b.jpg"), new ProductImageSortOrder(2)));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductImageAlreadyExistsException>();
    }

    [Fact]
    public void AddImage_Throws_ProductImageSortOrderAlreadyTakenException_When_Sort_Order_Is_Taken()
    {
        var product = GetProduct();
        product.AddImage(new ProductImageId(Guid.NewGuid()),
            new ProductImageUrl("https://cdn.test/a.jpg"),
            new ProductImageSortOrder(1));

        var exception = Record.Exception(() => product.AddImage(
            new ProductImageId(Guid.NewGuid()),
            new ProductImageUrl("https://cdn.test/b.jpg"),
            new ProductImageSortOrder(1)));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductImageSortOrderAlreadyTakenException>();
    }

    [Fact]
    public void AddImage_Throws_ProductImageLimitExceededException_When_Limit_Reached()
    {
        var product = GetProduct();
        for (var i = 0; i < Product.MaxImages; i++)
        {
            product.AddImage(new ProductImageId(Guid.NewGuid()),
                new ProductImageUrl($"https://cdn.test/{i}.jpg"),
                new ProductImageSortOrder(i + 1));
        }

        var exception = Record.Exception(() => product.AddImage(
            new ProductImageId(Guid.NewGuid()),
            new ProductImageUrl("https://cdn.test/extra.jpg"),
            new ProductImageSortOrder(Product.MaxImages + 1)));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductImageLimitExceededException>();
    }

    [Fact]
    public void AddImage_Adds_ProductImageAddedEvent_Domain_Event_On_Success()
    {
        var product = GetProduct();
        var imageId = new ProductImageId(Guid.NewGuid());

        var exception = Record.Exception(() => product.AddImage(imageId,
            new ProductImageUrl("https://cdn.test/a.jpg"),
            new ProductImageSortOrder(1)));

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);
        var @event = product.Events.FirstOrDefault() as ProductImageAddedEvent;
        @event.ShouldNotBeNull();
        @event.ImageId.Value.ShouldBe(imageId.Value);
    }

    [Fact]
    public void RemoveImage_Throws_ProductImageNotFoundException_When_Image_Does_Not_Exist()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.RemoveImage(new ProductImageId(Guid.NewGuid())));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductImageNotFoundException>();
    }

    [Fact]
    public void RemoveImage_Throws_CannotRemoveLastImageOfPublishedProductException_When_Removing_Last_Image_From_Published_Product()
    {
        var product = GetProductWithImage();
        product.Publish();
        var lastImageId = product.Images.First().Id;

        var exception = Record.Exception(() => product.RemoveImage(lastImageId));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CannotRemoveLastImageOfPublishedProductException>();
    }

    [Fact]
    public void RemoveImage_Adds_ProductImageRemovedEvent_Domain_Event_On_Success()
    {
        var product = GetProduct();
        var imageId = new ProductImageId(Guid.NewGuid());
        product.AddImage(imageId, new ProductImageUrl("https://cdn.test/a.jpg"), new ProductImageSortOrder(1));
        product.ClearEvents();

        var exception = Record.Exception(() => product.RemoveImage(imageId));

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);
        var @event = product.Events.FirstOrDefault() as ProductImageRemovedEvent;
        @event.ShouldNotBeNull();
        @event.ImageId.Value.ShouldBe(imageId.Value);
    }

    [Fact]
    public void ChangeImageSortOrder_Throws_ProductImageNotFoundException_When_Image_Does_Not_Exist()
    {
        var product = GetProduct();

        var exception = Record.Exception(() =>
            product.ChangeImageSortOrder(new ProductImageId(Guid.NewGuid()), new ProductImageSortOrder(1)));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductImageNotFoundException>();
    }

    [Fact]
    public void ChangeImageSortOrder_Throws_ProductImageSortOrderAlreadyTakenException_When_Other_Image_Has_That_Sort_Order()
    {
        var product = GetProduct();
        var firstId = new ProductImageId(Guid.NewGuid());
        var secondId = new ProductImageId(Guid.NewGuid());
        product.AddImage(firstId, new ProductImageUrl("https://cdn.test/a.jpg"), new ProductImageSortOrder(1));
        product.AddImage(secondId, new ProductImageUrl("https://cdn.test/b.jpg"), new ProductImageSortOrder(2));

        var exception = Record.Exception(() =>
            product.ChangeImageSortOrder(firstId, new ProductImageSortOrder(2)));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductImageSortOrderAlreadyTakenException>();
    }

    [Fact]
    public void ChangeImageSortOrder_Does_Not_Add_Event_When_Sort_Order_Is_Not_Changed()
    {
        var product = GetProduct();
        var imageId = new ProductImageId(Guid.NewGuid());
        product.AddImage(imageId, new ProductImageUrl("https://cdn.test/a.jpg"), new ProductImageSortOrder(1));
        product.ClearEvents();

        product.ChangeImageSortOrder(imageId, new ProductImageSortOrder(1));

        product.Events.ShouldBeEmpty();
    }

    [Fact]
    public void ChangeImageSortOrder_Adds_ProductImageSortOrderChangedEvent_Domain_Event_On_Success()
    {
        var product = GetProduct();
        var imageId = new ProductImageId(Guid.NewGuid());
        product.AddImage(imageId, new ProductImageUrl("https://cdn.test/a.jpg"), new ProductImageSortOrder(1));
        product.ClearEvents();

        var exception = Record.Exception(() =>
            product.ChangeImageSortOrder(imageId, new ProductImageSortOrder(7)));

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);
        var @event = product.Events.FirstOrDefault() as ProductImageSortOrderChangedEvent;
        @event.ShouldNotBeNull();
        @event.SortOrder.Value.ShouldBe(7);
    }

    [Fact]
    public void Delete_Throws_CannotDeletePublishedProductException_When_Product_Is_Published()
    {
        var product = GetProductWithImage();
        product.Publish();

        var exception = Record.Exception(() => product.Delete());

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CannotDeletePublishedProductException>();
    }

    [Fact]
    public void Delete_Adds_ProductDeletedEvent_Domain_Event_On_Success()
    {
        var product = GetProduct();

        var exception = Record.Exception(() => product.Delete());

        exception.ShouldBeNull();
        product.Events.Count().ShouldBe(1);
        product.Events.FirstOrDefault().ShouldBeOfType<ProductDeletedEvent>();
    }


    #region ARRANGE

    private Product GetProduct()
    {
        var product = _productFactory.Create(
            new ProductId(Guid.NewGuid()),
            new ProductName("Test Product"),
            new ProductSlug("test-product"),
            new ProductPrice(99.99m),
            new CategoryId(Guid.NewGuid()),
            new ProductSortOrder(1));

        product.ClearEvents();
        return product;
    }

    private Product GetProductWithImage()
    {
        var product = GetProduct();
        product.AddImage(
            new ProductImageId(Guid.NewGuid()),
            new ProductImageUrl("https://cdn.test/main.jpg"),
            new ProductImageSortOrder(1));
        product.ClearEvents();
        return product;
    }

    private readonly IProductFactory _productFactory;

    public ProductTests()
    {
        _productFactory = new ProductFactory();
    }

    #endregion
}

using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategorySortOrder
{
    public int Value { get; }

    public CategorySortOrder(int value)
    {
        if (value < 0)
        {
            throw new NegativeCategorySortOrderException();
        }
        
        Value = value;
    }
    
    public static implicit operator int(CategorySortOrder sortOrder) => sortOrder.Value;
    public static implicit operator CategorySortOrder(int sortOrder) => new CategorySortOrder(sortOrder);
}
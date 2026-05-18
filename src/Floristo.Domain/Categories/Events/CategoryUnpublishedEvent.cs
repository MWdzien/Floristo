using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Events;

public record CategoryUnpublishedEvent(CategoryId Id, CategoryUnpublishedAt UnpublishedAt) : IDomainEvent;
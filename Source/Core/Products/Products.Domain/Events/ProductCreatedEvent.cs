namespace shop.services.products.domain.Events;

public class ProductCreatedEvent(Guid id, string description) : IDomainEvent
{
	public Guid Id { get; private set; } = id;
	public string Description { get; private set; } = description;
}
namespace shop.services.products.domain.Events;

public class ProductUpdatedEvent(Guid id) : IDomainEvent
{
	public Guid Id { get; private set; } = id;
}
namespace shop.services.customers.domain.Events;

public class AddressUpdatedEvent(Guid customerId, string fullAddress) : IDomainEvent
{
	public Guid CustomerId { get; private set; } = customerId;
	public string FullAddress { get; private set; } = fullAddress;
}
namespace Customer.Domain.Events;

public class AddressUpdatedEvent(Guid customerId, string fullAddress) : IDomainEvent
{
	public Guid CustomerId { get; private set; } = customerId;
	public string FullAddress { get; private set; } = fullAddress;
}
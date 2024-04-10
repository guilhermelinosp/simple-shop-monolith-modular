using Customer.Domain.VOs;

namespace Customer.Domain.Events;

public class CustomerUpdatedEvent : IDomainEvent
{
	public CustomerUpdatedEvent(Guid id, string phoneNumber, Address address)
	{
		Id = id;
		Address = address;
	}

	public Guid Id { get; private set; }
	public Address Address { get; private set; }
}
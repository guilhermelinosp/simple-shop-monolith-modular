namespace shop.services.customers.domain.Events;

public class CustomerCreatedEvent(Guid id, string fullName, string email) : IDomainEvent
{
	public Guid Id { get; private set; } = id;
	public string FullName { get; private set; } = fullName;
	public string Email { get; private set; } = email;
}
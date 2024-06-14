using Order.Domain.VOs;

namespace Order.Domain.Events;

public class OrderCreatedEvent(Guid id, decimal total, PaymentInfo paymentInfo, string name, string email)
	: IDomainEvent
{
	public Guid Id { get; } = id;
	public decimal Total { get; } = total;
	public PaymentInfo PaymentInfo { get; } = paymentInfo;
	public string Name { get; } = name;
	public string Email { get; } = email;
}
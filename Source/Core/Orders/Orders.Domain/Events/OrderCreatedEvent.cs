using shop.services.orders.domain.VOs;

namespace shop.services.orders.domain.Events
{
	public class OrderCreatedEvent(Guid id, decimal total, PaymentMethod paymentMethod, string name, string email)
		: IDomainEvent
	{
		public Guid Id { get; } = id;
		public decimal Total { get; } = total;
		public PaymentMethod PaymentMethod { get; } = paymentMethod;
		public string Name { get; } = name;
		public string Email { get; } = email;
	}
}
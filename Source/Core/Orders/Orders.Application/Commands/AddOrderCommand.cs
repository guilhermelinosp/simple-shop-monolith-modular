using MediatR;
using Order.Domain.Entities;
using Order.Domain.VOs;

namespace Orders.Application.Commands;

public class AddOrderCommand(
	CustomerRequest customer,
	List<OrderItemRequest> items,
	DeliveryAddressRequest deliveryAddress,
	PaymentAddressRequest paymentAddress,
	PaymentInfoRequest paymentInfo)
	: IRequest<Guid>
{
	public CustomerRequest Customer { get; set; } = customer;
	public List<OrderItemRequest> Items { get; set; } = items;
	public DeliveryAddressRequest DeliveryAddress { get; set; } = deliveryAddress;
	public PaymentAddressRequest PaymentAddress { get; set; } = paymentAddress;
	public PaymentInfoRequest PaymentInfo { get; set; } = paymentInfo;

	public Order.Domain.Entities.Order ToEntity()
	{
		return new Order.Domain.Entities.Order(
			new Customer(Customer.Id, Customer.FullName, Customer.Email),
			new DeliveryAddress(DeliveryAddress.Street, DeliveryAddress.Number, DeliveryAddress.City,
				DeliveryAddress.State, DeliveryAddress.ZipCode),
			new PaymentAddress(PaymentAddress.Street, PaymentAddress.Number, PaymentAddress.City, PaymentAddress.State,
				PaymentAddress.ZipCode),
			new PaymentInfo(PaymentInfo.CardNumber, PaymentInfo.FullName, PaymentInfo.ExpirationDate, PaymentInfo.Cvv),
			Items.Select(i => new OrderItem(i.ProductId, i.Quantity, i.Price)).ToList()
		);
	}
}

public class CustomerRequest
{
	public Guid Id { get; set; }
	public required string FullName { get; set; }
	public required string Email { get; set; }
}

public class OrderItemRequest
{
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
	public decimal Price { get; set; }
}

public class DeliveryAddressRequest
{
	public required string Street { get; set; }
	public required string Number { get; set; }
	public required string City { get; set; }
	public required string State { get; set; }
	public required string ZipCode { get; set; }
}

public class PaymentAddressRequest
{
	public required string Street { get; set; }
	public required string Number { get; set; }
	public required string City { get; set; }
	public required string State { get; set; }
	public required string ZipCode { get; set; }
}

public class PaymentInfoRequest
{
	public required string CardNumber { get; set; }
	public required string FullName { get; set; }
	public required string ExpirationDate { get; set; }
	public required string Cvv { get; set; }
}
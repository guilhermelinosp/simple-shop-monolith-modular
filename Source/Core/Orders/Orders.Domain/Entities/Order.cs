using MongoDB.Bson.Serialization.Attributes;
using Order.Domain.Enums;
using Order.Domain.Events;
using Order.Domain.VOs;

namespace Order.Domain.Entities;

public class Order : Aggregate
{
	[BsonElement("_id")]
	public new Guid Id { get; private set; }
	public decimal Total { get; private set; }
	public Customer Customer { get; private set; }
	public DeliveryAddress DeliveryAddress { get; private set; }
	public PaymentAddress PaymentAddress { get; private set; }
	public PaymentInfo PaymentInfo { get; private set; }
	public List<OrderItem> Items { get; private set; }
	public OrderStatus Status { get; private set; }
	[BsonElement("created_at")]
	public new DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

	// Constructor
	public Order(Customer customer, DeliveryAddress deliveryAddress, PaymentAddress paymentAddress,
		PaymentInfo paymentInfo, List<OrderItem> items)
	{
		Id = Guid.NewGuid();
		Customer = customer;
		DeliveryAddress = deliveryAddress;
		PaymentAddress = paymentAddress;
		PaymentInfo = paymentInfo;
		Items = items;
		Status = OrderStatus.Started;
		Total = Items.Sum(i => i.Quantity * i.Price);
		AddEvent(new OrderCreatedEvent(Id, Total, paymentInfo, Customer.Name, Customer.Email));
	}

	public Order(Guid id, Customer customer, DeliveryAddress deliveryAddress, PaymentAddress paymentAddress,
		PaymentInfo paymentInfo, List<OrderItem> items)
	{
		Id = id;
		Customer = customer;
		DeliveryAddress = deliveryAddress;
		PaymentAddress = paymentAddress;
		PaymentInfo = paymentInfo;
		Items = items;
	}

	// Methods
	public void SetAsCompleted()
	{
		Status = OrderStatus.Completed;
	}

	public void SetAsRejected()
	{
		Status = OrderStatus.Rejected;
	}
}
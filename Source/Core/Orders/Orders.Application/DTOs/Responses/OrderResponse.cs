namespace Orders.Application.DTOs.Responses;

public record OrderResponse(Guid id, decimal totalPrice, DateTime createdAt, string status)
{
	public Guid Id { get; private set; } = id;
	public decimal TotalPrice { get; private set; } = totalPrice;
	public DateTime CreatedAt { get; private set; } = createdAt;
	public string Status { get; private set; } = status;

	public static OrderResponse FromEntity(Order.Domain.Entities.Order order) {
		return new OrderResponse(order.Id, order.Total, order.CreatedAt, order.Status.ToString());
	}
	
}
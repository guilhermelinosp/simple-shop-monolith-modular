using MediatR;
using Orders.Application.DTOs.Responses;

namespace Orders.Application.Queries;

public class GetOrderByIdQuery(Guid id) : IRequest<OrderResponse>
{
	public Guid Id { get; set; } = id;
}
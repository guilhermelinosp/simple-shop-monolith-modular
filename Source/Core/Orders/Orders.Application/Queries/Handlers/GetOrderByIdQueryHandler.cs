using MediatR;
using Order.Domain.Repositories;
using Orders.Application.DTOs.Responses;

namespace Orders.Application.Queries.Handlers;

public class GetOrderByIdQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderByIdQuery, OrderResponse>
{
	public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
	{
		var order = await orderRepository.FindByIdAsync(request.Id);

		return OrderResponse.FromEntity(order);
	}
}
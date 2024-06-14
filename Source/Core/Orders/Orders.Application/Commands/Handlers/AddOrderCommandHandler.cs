using MediatR;
using Order.Domain.Repositories;
using Order.Infrastructure.MessageBus;
using Order.Infrastructure.ServiceDiscovery;

namespace Orders.Application.Commands.Handlers;

public class AddOrderCommandHandler(
	IOrderRepository orderRepository,
	IMessageBusClient messageBus,
	IServiceDiscoveryService serviceDiscovery)
	: IRequestHandler<AddOrderCommand, Guid>
{
	private readonly IMessageBusClient _messageBus = messageBus;
	private readonly IServiceDiscoveryService _serviceDiscovery = serviceDiscovery;

	public async Task<Guid> Handle(AddOrderCommand request, CancellationToken cancellationToken)
	{
		var order = request.ToEntity();
		await orderRepository.CreateAsync(order);

		foreach (var @event in order.Events)
		{
			var routingKey = @event.GetType().Name.ToDashCase();
  
			_messageBus.Publish(@event, routingKey, "order-service");
		}

		return order.Id;
	}
}

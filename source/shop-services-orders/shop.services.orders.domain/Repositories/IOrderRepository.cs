using shop.services.orders.domain.Entities;

namespace shop.services.orders.domain.Repositories;

public interface IOrderRepository
{
	Task<Order> FindByIdAsync(Guid id);
	Task CreateAsync(Order order);
	Task UpdateAsync(Order order);
}
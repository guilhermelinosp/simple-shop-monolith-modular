namespace Order.Domain.Repositories;

public interface IOrderRepository
{
	Task<Entities.Order> FindByIdAsync(Guid id);
	Task CreateAsync(Entities.Order order);
	Task UpdateAsync(Entities.Order order);
}
using MongoDB.Driver;
using Order.Domain.Repositories;

namespace Order.Infrastructure.Persistence.Repositories;

public class OrderRepository(IMongoDatabase mongoDatabase) : IOrderRepository
{
	private readonly IMongoCollection<Domain.Entities.Order> _collection =
		mongoDatabase.GetCollection<Domain.Entities.Order>("orders");

	public async Task CreateAsync(Domain.Entities.Order order)
	{
		await _collection.InsertOneAsync(order);
	}

	public async Task<Domain.Entities.Order> FindByIdAsync(Guid id)
	{
		return await _collection.Find(c => c.Id == id).SingleOrDefaultAsync();
	}

	public async Task UpdateAsync(Domain.Entities.Order order)
	{
		await _collection.ReplaceOneAsync(c => c.Id == order.Id, order);
	}
}
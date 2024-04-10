using Products.Domain.Entities;

namespace Products.Domain.Repositories;

public interface IProductRepository
{
	Task<List<Product>> FindAllAsync();
	Task<Product> FindByIdAsync(Guid id);
	Task CreateAsync(Product product);
	Task UpdateAsync(Product product);
}
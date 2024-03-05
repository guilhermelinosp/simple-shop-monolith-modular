using shop.services.products.domain.Entities;

namespace shop.services.products.domain.Repositories;

public interface IProductRepository
{
	Task<List<Product>> FindAllAsync();
	Task<Product> FindByIdAsync(Guid id);
	Task CreateAsync(Product product);
	Task UpdateAsync(Product product);
}
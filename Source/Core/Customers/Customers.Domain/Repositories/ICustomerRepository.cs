using shop.services.customers.domain.Entities;

namespace shop.services.customers.domain.Repositories;

public interface ICustomerRepository
{
	Task<Customer> FindByIdAsync(Guid id);
	Task CreateAsync(Customer customer);
	Task UpdateAsync(Customer customer);
}
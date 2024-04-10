namespace Customer.Domain.Repositories;

public interface ICustomerRepository
{
	Task<Entities.Customer> FindByIdAsync(Guid id);
	Task CreateAsync(Entities.Customer customer);
	Task UpdateAsync(Entities.Customer customer);
}
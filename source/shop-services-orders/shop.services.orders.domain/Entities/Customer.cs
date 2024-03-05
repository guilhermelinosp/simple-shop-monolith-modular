namespace shop.services.orders.domain.Entities;

public class Customer(Guid id, string name, string email)
{
	public Guid Id { get; private set; } = id;
	public string Name { get; private set; } = name;
	public string Email { get; private set; } = email;
	public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
}
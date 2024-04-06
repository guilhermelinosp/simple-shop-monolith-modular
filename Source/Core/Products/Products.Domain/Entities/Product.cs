using shop.services.products.domain.Events;
using shop.services.products.domain.VOs;

namespace shop.services.products.domain.Entities;

public class Product(string title, string description, decimal price, int quantity, Category? category)
	: Aggregate
{
	public string Title { get; private set; } = title;
	public string Description { get; private set; } = description;
	public decimal Price { get; private set; } = price;
	public int Quantity { get; private set; } = quantity;
	public Category? Category { get; private set; } = category;

	public void Update(string description, decimal price, Category? category)
	{
		if (category != null)
		{
			Category = category;
		}

		Description = description;
		Price = price;

		AddEvent(new ProductUpdatedEvent(Id));
	}

	public static Product Create(string title, string description, decimal price, int quantity, Category? category)
	{
		return new Product(title, description, price, quantity, category);;
	}
}
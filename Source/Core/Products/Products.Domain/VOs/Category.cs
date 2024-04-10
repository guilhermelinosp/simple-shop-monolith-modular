namespace Products.Domain.VOs;

public class Category(string description, string subcategory)
{
	// Properties
	public string Description { get; } = description;
	public string Subcategory { get; } = subcategory;

	// Methods
	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType())
			return false;

		var other = (Category)obj;
		return Description == other.Description && Subcategory == other.Subcategory;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Description, Subcategory);
	}
}
namespace Order.Domain.VOs;

public class PaymentAddress(string street, string number, string city, string state, string zipCode)
{
	public string Street { get; } = street;
	public string Number { get; } = number;
	public string City { get; } = city; 
	public string State { get; } = state;
	public string ZipCode { get; } = zipCode;

	public override bool Equals(object? obj)
	{
		return obj is DeliveryAddress address &&
		       Street == address.Street &&
		       Number == address.Number &&
		       City == address.City &&
		       State == address.State &&
		       ZipCode == address.ZipCode;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Street, Number, City, State, ZipCode);
	}
}
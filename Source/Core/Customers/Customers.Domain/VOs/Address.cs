using System;

namespace shop.services.customers.domain.VOs;

public class Address(string street, string number, string city, string state, string zipCode)
	: IEquatable<Address>
{
	// Properties
	public string Street { get; } = street;
	public string Number { get; } = number;
	public string City { get; } = city;
	public string State { get; } = state;
	public string ZipCode { get; } = zipCode;

	// Constructor

	// Methods
	public string GetFullAddress()
	{
		return $"{Street}, {Number}, {City}, {State}, {ZipCode}";
	}

	public bool Equals(Address? other)
	{
		if (other == null)
			return false;

		return string.Equals(Street, other.Street, StringComparison.OrdinalIgnoreCase) &&
		       string.Equals(Number, other.Number, StringComparison.OrdinalIgnoreCase) &&
		       string.Equals(City, other.City, StringComparison.OrdinalIgnoreCase) &&
		       string.Equals(State, other.State, StringComparison.OrdinalIgnoreCase) &&
		       string.Equals(ZipCode, other.ZipCode, StringComparison.OrdinalIgnoreCase);
	}

	public override bool Equals(object? obj)
	{
		return obj is Address address && Equals(address);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(
			StringComparer.OrdinalIgnoreCase.GetHashCode(Street),
			StringComparer.OrdinalIgnoreCase.GetHashCode(Number),
			StringComparer.OrdinalIgnoreCase.GetHashCode(City),
			StringComparer.OrdinalIgnoreCase.GetHashCode(State),
			StringComparer.OrdinalIgnoreCase.GetHashCode(ZipCode));
	}
}
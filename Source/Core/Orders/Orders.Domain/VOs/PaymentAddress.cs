using System;

namespace shop.services.orders.domain.VOs
{
	public class PaymentAddress(string street, string number, string city, string state, string zipCode)
	{
		// Properties
		public string Street { get; } = street;
		public string Number { get; } = number ;
		public string City { get; } = city;
		public string State { get; } = state;
		public string ZipCode { get; } = zipCode;
		
		// Overrides
		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			var other = (PaymentAddress)obj;
			return Street == other.Street &&
			       Number == other.Number &&
			       City == other.City &&
			       State == other.State &&
			       ZipCode == other.ZipCode;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Street, Number, City, State, ZipCode);
		}
	}
}
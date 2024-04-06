using System;

namespace shop.services.orders.domain.VOs
{
	public class PaymentMethod(string cardNumber, string fullName, string expiration, string cvv)
	{
		// Properties
		public string CardNumber { get; } = cardNumber;
		public string FullName { get; } = fullName;
		public string Expiration { get; } = expiration;
		public string Cvv { get; } = cvv;
		
		// Overrides
		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			var other = (PaymentMethod)obj;
			return CardNumber == other.CardNumber &&
			       FullName == other.FullName &&
			       Expiration == other.Expiration &&
			       Cvv == other.Cvv;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(CardNumber, FullName, Expiration, Cvv);
		}
	}
}